using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public float maxHealth = 100;
    public float currentHealth;
    public event Action OnDeath;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Destroy(this);
        }
    }
    
}
public interface IDamagable
{
    void TakeDamage(float damage);
}
