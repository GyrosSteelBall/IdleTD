using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<int, int> OnHealthChanged;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    public void TakeDamage(int damage)
    {
        int oldHealth = CurrentHealth;
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        OnHealthChanged?.Invoke(oldHealth, CurrentHealth);
        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public void Heal(int amount)
    {
        int oldHealth = CurrentHealth;
        // If heal amount is greater than max health, set HP to max health
        if ((CurrentHealth + amount) >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += amount;
        }
        // Trigger the event if health changed
        if (oldHealth != CurrentHealth)
        {
            OnHealthChanged?.Invoke(oldHealth, CurrentHealth);
        }
    }

    public void SetMaxHealth(int amount)
    {
        MaxHealth = amount;
    }

    public void SetCurrentHealth(int amount)
    {
        int oldHealth = CurrentHealth;
        CurrentHealth = amount;
        // Trigger the event if health changed
        if (oldHealth != CurrentHealth)
        {
            OnHealthChanged?.Invoke(oldHealth, CurrentHealth);
        }
    }
}

