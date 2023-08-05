using UnityEngine;

public abstract class Stats : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] protected float maxHealthPoints;
    protected float currentHealthPoints;

    protected virtual void Awake()
    {
        currentHealthPoints = maxHealthPoints;
    }

    public virtual void TakeDamage(float value)
    {
        currentHealthPoints -= value;

        if (currentHealthPoints <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
