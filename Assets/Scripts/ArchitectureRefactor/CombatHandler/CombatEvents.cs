using UnityEngine;
using System;

public class CombatEvents : MonoBehaviour
{
    public static CombatEvents Instance { get; private set; }

    // Events that observers can subscribe to
    public event Action<object, IAttackable, float> OnDamageDealt;
    public event Action<IAttackable> OnEntityDefeated;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // The method to call when damage is dealt
    public void DamageDealt(object source, IAttackable target, float damage)
    {
        OnDamageDealt?.Invoke(source, target, damage);
    }

    // The method to call when an entity is defeated
    public void EntityDefeated(IAttackable entity)
    {
        OnEntityDefeated?.Invoke(entity);
    }
}
