using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private bool isDead;

    public event Action<int, int> OnHealthChanged;

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetCurrentHealth(int value)
    {
        int oldHealth = currentHealth;
        currentHealth = value;
        if (oldHealth != currentHealth)
        {
            OnHealthChanged?.Invoke(oldHealth, currentHealth);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetIsDead(bool value)
    {
        isDead = value;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        int oldHealth = currentHealth;
        SetCurrentHealth(Mathf.Max(0, currentHealth - damage));
        if (currentHealth <= 0)
        {
            SetIsDead(true);
        }
    }

    public void Heal(int amount)
    {
        int oldHealth = currentHealth;
        // If heal amount is greater than max health, set HP to max health
        if ((currentHealth + amount) >= maxHealth)
        {
            SetCurrentHealth(maxHealth);
        }
        else
        {
            SetCurrentHealth(currentHealth + amount);
        }
        // Trigger the event if health changed
        if (oldHealth != currentHealth)
        {
            OnHealthChanged?.Invoke(oldHealth, currentHealth);
        }
    }
}
