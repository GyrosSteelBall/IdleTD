public class CombatSystemApplyDamageToUnitEvent
{
    public UnitController Target { get; set; }
    public int Damage { get; set; }

    public CombatSystemApplyDamageToUnitEvent(UnitController target, int damage)
    {
        Target = target;
        Damage = damage;
    }
}