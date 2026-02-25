using UnityEngine;
using UnityEngine.AI;

public class SpawnerMonstres : MonoBehaviour
{
    public GameObject prefabMonstre;
    public int nombreMaximum = 10;
    public float rayonSpawn;
    public float delaiEntreSpawns = 2f;
    private int _monstresActuels = 0;

    void Start()
    {
        InvokeRepeating(nameof(ApparitionAleatoire), 1f, delaiEntreSpawns);
    }

    void ApparitionAleatoire()
    {
        if (_monstresActuels >= nombreMaximum) return;

        Vector3 positionValide = Vector3.zero;
        bool pointTrouve = false;
        int tentatives = 0;

        // On essaye jusqu'à 10 fois pour ne pas bloquer le jeu si la zone est trop petite
        while (!pointTrouve && tentatives < 10)
        {
            Vector3 randomDirection = Random.insideUnitSphere * rayonSpawn;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
            {
                positionValide = hit.position;
                pointTrouve = true;
            }
            tentatives++;
        }

        if (pointTrouve)
        {
            Instantiate(prefabMonstre, positionValide, Quaternion.identity);
            _monstresActuels++;
        }
        else 
        {
            // On ne log que si vraiment on n'a rien trouvé après 10 essais
            Debug.LogWarning("Le Spawner est peut-être trop loin de la zone marchable.");
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rayonSpawn);
    }
}