using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    // Singleton pattern implementation
    public static CombatHandler Instance { get; private set; }

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

    // Method called when direct damage is dealt
    public void ProcessDirectDamage(IAttackable target, float damage)
    {
        target.TakeDamage(damage);
        CombatEvents.Instance.DamageDealt(this, target, damage);

        if (target.Health <= 0)
        {
            target.Defeat();
            // Broadcast the defeat event
            CombatEvents.Instance.EntityDefeated(target);
        }
    }

    // Add other methods for AOE, DOTs, and chaining as necessary
}
