public class CombatSystemApplyDamageToUnitEvent
{
    public UnitController Target { get; set; }
    public float Damage { get; set; }

    public CombatSystemApplyDamageToUnitEvent(UnitController target, float damage)
    {
        Target = target;
        Damage = damage;
    }
}