using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int puissanceAttaque = 10;
    public float cadenceAttaque = 1.5f;
    private float _prochaineAttaque = 0f;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= _prochaineAttaque)
            {
                PlayerHealth sante = other.GetComponent<PlayerHealth>();
                if (sante is not null)
                {
                    sante.TakeDamage(puissanceAttaque, transform.position);
                    _prochaineAttaque = Time.time + cadenceAttaque;
                }
            }
        }
    }
}
