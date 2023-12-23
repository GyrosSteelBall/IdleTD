public class UnitControllerAttackEvent
{
    public UnitController Attacker { get; set; }
    public EnemyController Target { get; set; }
    public int RawDamage { get; set; }

    public UnitControllerAttackEvent(UnitController attacker, EnemyController target, int rawDamage)
    {
        Attacker = attacker;
        Target = target;
        RawDamage = rawDamage;
    }
}