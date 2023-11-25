public enum StatusEffectType
{
    //Put status effect types here
}

public interface IStatusEffect
{
    StatusEffectType StatusEffectType { get; }
    string Name { get; }
    float Duration { get; }
    bool IsPermanent { get; }
    bool IsStackable { get; }
    bool IsRefreshable { get; }
    void ApplyEffect(IAttackable target);
    void RemoveEffect(IAttackable target);
    void TickEffect(IAttackable target); // Called periodically if the effect has a duration
}