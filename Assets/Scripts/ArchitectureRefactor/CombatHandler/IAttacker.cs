public enum DamageType
{
    Physical,
    Magical
}

public interface IAttacker
{
    DamageType DamageType { get; }
    ElementType ElementType { get; }
    float CriticalHitChance { get; } // Chances of a critical hit, expressed as a percentage.
    float CriticalHitMultiplier { get; } // Damage multiplier for critical hits.
    // ... Other attack properties like attack power, attack speed, etc.
}