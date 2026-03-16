using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack")]
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private float damage = 8f;

    [Header("Hit")]
    [SerializeField] private float hitRadius = 1.5f;

    [Header("VFX")]
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float spawnDistance = 1.2f;
    [SerializeField] private float heightOffset = 1.0f;
    [SerializeField] private float vfxLifetime = 2f;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        Transform target = FindClosestEnemy();
        if (target is null) return;

        Attack(target);
        _timer = attackInterval;
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
        // Direction vers la cible
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
            dir = transform.forward;

        Quaternion rot = Quaternion.LookRotation(dir.normalized, Vector3.up);

        // Position d'apparition du slash
        Vector3 pos = transform.position + rot * Vector3.forward * spawnDistance;
        pos.y += heightOffset;

        // Spawn VFX
        if (slashPrefab is not null)
        {
            GameObject vfx = Instantiate(slashPrefab, pos, rot);

            // Adapte la taille du VFX à la zone d'attaque
            vfx.transform.localScale *= hitRadius;

            Destroy(vfx, vfxLifetime);
        }

        // Zone de dégâts
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
            if (hp is not null)
            {
                hp.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}