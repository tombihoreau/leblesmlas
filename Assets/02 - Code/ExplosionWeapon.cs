using UnityEngine;

public class ExplosionWeapon : Weapon
{
    [Header("Targeting")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Hit")]
    [SerializeField] private float hitRadius = 3f;
    [Header("VFX")]
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float heightOffset = 2.0f;
    [SerializeField] private float vfxLifetime = 2f;

    protected override void TryAttack()
    {
        Debug.Log("ExplosionWeapon TryAttack");

        Transform target = FindClosestEnemy();
        if (target == null)
        {
            Debug.Log("ExplosionWeapon : aucune cible");
            return;
        }

        Debug.Log("ExplosionWeapon : cible trouvée " + target.name);
        Attack(target);
    }

    private Transform FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer, QueryTriggerInteraction.Ignore);
        if (hits.Length == 0) return null;

        Transform best = null;
        float bestDist = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            float d = (hits[i].transform.position - transform.position).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = hits[i].transform;
            }
        }

        return best;
    }

    private void Attack(Transform target)
    {
        Vector3 pos = target.position;
        pos.y += heightOffset;

        if (slashPrefab != null)
        {
            GameObject vfx = Instantiate(slashPrefab, pos, Quaternion.identity);
            vfx.transform.localScale *= hitRadius;
            Destroy(vfx, vfxLifetime);
        }

        Collider[] victims = Physics.OverlapSphere(
            target.position,
            hitRadius,
            enemyLayer,
            QueryTriggerInteraction.Ignore
        );

        Debug.Log("ExplosionWeapon victims count: " + victims.Length);

        for (int i = 0; i < victims.Length; i++)
        {
            EnemyHealth hp = victims[i].GetComponentInParent<EnemyHealth>();
            if (hp != null)
            {
                Debug.Log("ExplosionWeapon hit : " + victims[i].name);
                hp.TakeDamage(damage);
            }
        }
    }

    public void IncreaseAttackRange(float amount)
    {
        attackRange += amount;
        Debug.Log("Sword attack range augmentée : " + attackRange);
    }

    public void IncreaseAttackSize(float amount)
    {
        hitRadius += amount;
        Debug.Log("Sword hit radius augmentée : " + hitRadius);
    }
}