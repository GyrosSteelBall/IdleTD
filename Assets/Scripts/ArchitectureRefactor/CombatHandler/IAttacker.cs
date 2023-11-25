public interface IAttacker
{
    ElementType ElementType { get; }
    float CriticalHitChance { get; } // Chances of a critical hit, expressed as a percentage.
    float CriticalHitMultiplier { get; } // Damage multiplier for critical hits.
    // ... Other attack properties like attack power, attack speed, etc.
}