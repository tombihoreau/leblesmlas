using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHp = 20f;
    private float _hp;
    public GameObject xpPrefab;
    [SerializeField] private float xpHeightOffset = 1f;
    private void Awake() => _hp = maxHp;

    public void TakeDamage(float dmg)
    {
        _hp -= dmg;
        if (_hp <= 0f)
        {
            Destroy(gameObject);
            SpawnXP();
        }
    }

    public void SpawnXP()
    {
        Vector3 spawnPos = transform.position + Vector3.up * xpHeightOffset;
        Instantiate(xpPrefab, spawnPos, Quaternion.identity);
    }
}