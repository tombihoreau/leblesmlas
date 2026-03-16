using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Common Weapon Stats")]
    [SerializeField] protected float attackInterval = 2f;
    [SerializeField] protected float damage = 8f;

    protected float _timer;

    protected virtual void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        TryAttack();
        _timer = attackInterval;
    }

    protected abstract void TryAttack();

    public virtual void IncreaseDamage(float amount)
    {
        damage += amount;
        Debug.Log(name + " damage augmenté : " + damage);
    }

    public virtual void IncreaseAttackSpeed(float percent)
    {
        attackInterval *= (1f - percent);

        if (attackInterval < 0.1f)
            attackInterval = 0.1f;

        Debug.Log(name + " attack interval réduit : " + attackInterval);
    }
}