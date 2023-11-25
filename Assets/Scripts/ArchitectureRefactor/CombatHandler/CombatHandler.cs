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

    void Update()
    {
        // Imagine a list or other collection containing references to all attackable entities
        // foreach (var entity in attackableEntities)
        // {
        //     entity.TickStatusEffects();
        // }
    }

    // May need to be moved once IAttacker is implemented
    public enum DamageType
    {
        Physical,
        Magical
    }

    // Method to check for a critical hit
    public bool IsCriticalHit(IAttacker attacker)
    {
        float randomValue = Random.Range(0.0f, 100.0f);
        return randomValue < attacker.CriticalHitChance;
    }

    // Method called when direct damage is dealt
    public void ProcessDirectDamage(IAttackable target, float rawDamage, DamageType damageType, IAttacker attacker)
    {
        if (target.IsInvulnerable)
        {
            return; // Skip processing if the target is invulnerable
        }

        // Check if the target evades the attack
        if (EvadeAttack(target))
        {
            // The attack was evaded; you can broadcast an event or take some other action as needed
            // For example: CombatEvents.Instance.AttackEvaded(target);
            return; // Skip further damage processing
        }

        float inputDamage = rawDamage;

        // Check if the attack is a critical hit
        if (IsCriticalHit(attacker))
        {
            // Apply the critical hit multiplier to the damage
            inputDamage *= attacker.CriticalHitMultiplier;
            // Optionally, you could trigger a critical hit event or feedback here
            // e.g., CombatEvents.Instance.CriticalHitOccurred(attacker, target, damageToDeal);
        }

        // Apply percent damage reduction first, as this is the primary tanking stat
        float effectiveDamage = inputDamage * (1f - (target.PercentDamageReduction / 100f));

        // Apply elemental bonus or penalty based on the attacker's and target's elemental types
        effectiveDamage *= CalculateElementalBonus(attacker.ElementType, target.ElementalType);

        // Finally reduce the damage with Armor or Magic Resistance based on the DamageType
        switch (damageType)
        {
            case DamageType.Physical:
                effectiveDamage = ReduceDamageByResistance(effectiveDamage, target.Armor);
                break;
            case DamageType.Magical:
                effectiveDamage = ReduceDamageByResistance(effectiveDamage, target.MagicResistance);
                break;
        }

        // Apply the final calculated damage to the target
        target.TakeDamage(effectiveDamage);
    }

    // Method to determine if an attack hits or is evaded
    public bool EvadeAttack(IAttackable target)
    {
        // Generate a random number between 0 and 1
        float roll = Random.Range(0f, 100f);

        // If the roll is less than the target's evasion chance, the attack is evaded
        return roll < target.PercentEvadeChance;
    }

    // Helper method to calculate the damage reduction based on armor/magic resistance
    private float ReduceDamageByResistance(float damage, float resistance)
    {
        // Assumes resistance cannot go below zero and damage reduction formula is based on diminishing returns
        float mitigationFactor = resistance > 0 ? 100 / (100 + resistance) : 1;
        return damage * mitigationFactor;
    }

    // Helper method to determine the elemental bonus based on the attacker's and defender's types
    private float CalculateElementalBonus(ElementType attackerType, ElementType targetElementType)
    {
        // Here you would implement a lookup table to see if, for example, water deals extra damage to fire
        // The return value would be > 1 for bonus damage, 1 for neutral, and < 1 for reduced damage
        float elementalEffectiveness = ElementalEffectivenessLookup.GetEffectiveness(attackerType, targetElementType);
        return elementalEffectiveness;
    }

    // Apply a status effect to a target
    public void ApplyEffect(IAttackable target, IStatusEffect effect)
    {
        // Check if the effect is already applied and stackable or refreshable
        if (target.HasStatusEffect(effect.StatusEffectType) && effect.IsRefreshable)
        {
            // Refresh effect logic
        }
        else if (target.HasStatusEffect(effect.StatusEffectType) && effect.IsStackable)
        {
            // stack effect logic
        }
        else
        {
            // Apply the new effect to the target
            target.ApplyStatusEffect(effect);
            effect.ApplyEffect(target);
        }
    }

    public void RemoveEffect(IAttackable target, IStatusEffect effect)
    {
        target.RemoveStatusEffect(effect);
        effect.RemoveEffect(target); // Ensure clean-up logic is called, if any
    }
}
