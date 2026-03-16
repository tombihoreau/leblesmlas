using UnityEngine;

public class SwordWeapon : Weapon
{
    [Header("Targeting")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Hit")]
    [SerializeField] private float hitRadius = 1.5f;

    [Header("VFX")]
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float spawnDistance = 1.2f;
    [SerializeField] private float heightOffset = 1.0f;
    [SerializeField] private float vfxLifetime = 2f;

    protected override void TryAttack()
    {
        Transform target = FindClosestEnemy();
        if (target == null) return;

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
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f)
            dir = transform.forward;

        Quaternion rot = Quaternion.LookRotation(dir.normalized, Vector3.up);

        Vector3 pos = transform.position + rot * Vector3.forward * spawnDistance;
        pos.y += heightOffset;

        if (slashPrefab != null)
        {
            GameObject vfx = Instantiate(slashPrefab, pos, rot);
            vfx.transform.localScale *= hitRadius;
            Destroy(vfx, vfxLifetime);
        }

        Vector3 hitCenter = transform.position + rot * Vector3.forward * spawnDistance;

        Collider[] victims = Physics.OverlapSphere(
            hitCenter,
            hitRadius,
            enemyLayer,
            QueryTriggerInteraction.Ignore
        );

        for (int i = 0; i < victims.Length; i++)
        {
            EnemyHealth hp = victims[i].GetComponentInParent<EnemyHealth>();
            if (hp != null)
            {
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