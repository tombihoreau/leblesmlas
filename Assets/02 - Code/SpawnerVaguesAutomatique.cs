using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerVaguesAutomatique : MonoBehaviour
{
    
    public GameObject[] prefabsMonstres;
    public int nbMonstresInitial = 5;
    public float multiplicateurDifficulté = 1.2f;
    
    public float tempsEntreVagues = 30f;
    public float delaiEntreSpawns = 0.3f;
    
    public float rayonSpawn = 20f;

    private int _vagueActuelle = 0;

    void Start()
    {
        // On lance la boucle infinie des vagues
        StartCoroutine(BoucleDeJeu());
    }

    private IEnumerator BoucleDeJeu()
    {
        while (true)
        {
            _vagueActuelle++;

            int nbASpawn = Mathf.RoundToInt(nbMonstresInitial * Mathf.Pow(multiplicateurDifficulté, _vagueActuelle - 1));

            StartCoroutine(ApparitionVague(nbASpawn));
            
            yield return new WaitForSeconds(tempsEntreVagues);
        }
    }

    private IEnumerator ApparitionVague(int quantite)
    {
        for (int i = 0; i < quantite; i++)
        {
            ApparitionAleatoire();
            yield return new WaitForSeconds(delaiEntreSpawns);
        }
    }

    void ApparitionAleatoire()
    {
        if (prefabsMonstres == null || prefabsMonstres.Length == 0) return;

        Vector3 positionValide = Vector3.zero;
        bool pointTrouve = false;
        int tentatives = 0;

        while (!pointTrouve && tentatives < 15)
        {
            Vector3 randomDirection = Random.insideUnitSphere * rayonSpawn;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
            {
                positionValide = hit.position;
                pointTrouve = true;
            }
            tentatives++;
        }

        if (pointTrouve)
        {
            int indexAleatoire = Random.Range(0, prefabsMonstres.Length);
            GameObject monstreChoisi = prefabsMonstres[indexAleatoire];

            Instantiate(monstreChoisi, positionValide, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rayonSpawn);
    }
}