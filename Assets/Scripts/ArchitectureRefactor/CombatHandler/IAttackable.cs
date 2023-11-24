public enum ElementType
{
    Fire,
    Water,
    Earth,
    Lightning,
    Light,
    Dark,
    // Add as many as necessary for your game design
}

public interface IAttackable
{
    // Core properties
    float Health { get; }
    ElementType ElementalType { get; }
    float Armor { get; } // Used by CombatHandler for damage calculation
    float MagicResistance { get; } // Used by CombatHandler for damage calculation
    float PercentDamageReduction { get; } // Used by CombatHandler for damage calculation
    float PercentEvadeChance { get; } // Used by CombatHandler for damage calculation
    bool IsInvulnerable { get; }

    // Properties related to combat interaction
    int TauntLevel { get; } // Higher taunt levels might attract more enemy attention
    int VisibilityLevel { get; } // Affects the entity's ability to be detected by enemies

    // Combat methods
    void TakeDamage(float damage); // Amount of damage passed after calculation by CombatHandler
    void Heal(float amount);
    void Defeat();

    // Additional methods for applying and managing effects
    // void ApplyStatusEffect(IStatusEffect effect);
    // void ClearStatusEffects();
    // bool HasStatusEffect(Type effectType);
}