using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float forceRecul = 5f;
    private float _hp;
    
    private Renderer _renderer;
    private Rigidbody _rb;
    private SkinnedMeshRenderer _skinnedRenderer;

    private void Awake()
    {
        _hp = maxHp;
        _rb = GetComponent<Rigidbody>();
        _skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void TakeDamage(float dmg, Vector3 positionAgresseur)
    {
        _hp -= dmg;
        AppliquerRecul(positionAgresseur);
        
        StopAllCoroutines();
        StartCoroutine(FlashInvisibilite());

        if (_hp <= 0f) Debug.Log("Le Cowboy est mort...");
    }

    private void AppliquerRecul(Vector3 source)
    {
        if (_rb != null)
        {
            Vector3 direction = (transform.position - source).normalized;
            direction.y = 0.5f; 
            _rb.AddForce(direction * forceRecul, ForceMode.Impulse);
        }
    }

    private IEnumerator FlashInvisibilite()
    {
        if (_skinnedRenderer is null) yield break;
        
        for (int i = 0; i < 3; i++)
        {
            _skinnedRenderer.enabled = false; // Disparaît
            yield return new WaitForSeconds(0.05f);
            _skinnedRenderer.enabled = true; // Réapparaît
            yield return new WaitForSeconds(0.05f);
        }
    }
}