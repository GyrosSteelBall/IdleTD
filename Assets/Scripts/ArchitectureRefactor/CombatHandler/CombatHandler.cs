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

    // May need to be moved once IAttacker is implemented
    public enum DamageType
    {
        Physical,
        Magical
    }

    // Method called when direct damage is dealt
    public void ProcessDirectDamage(IAttackable target, float rawDamage, DamageType damageType, ElementType attackerElementType)
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

        // Apply percent damage reduction first, as this is the primary tanking stat
        float effectiveDamage = rawDamage * (1f - (target.PercentDamageReduction / 100f));

        // Apply elemental bonus or penalty based on the attacker's and target's elemental types
        effectiveDamage *= CalculateElementalBonus(attackerElementType, target.ElementalType);

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

    // Add other methods for AOE, DOTs, and chaining as necessary
}
