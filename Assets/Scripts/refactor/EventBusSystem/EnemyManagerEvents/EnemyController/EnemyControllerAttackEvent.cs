public class EnemyControllerAttackEvent
{
    public EnemyController Attacker { get; set; }
    public UnitController Target { get; set; }
    public int RawDamage { get; set; }

    public EnemyControllerAttackEvent(EnemyController attacker, UnitController target, int rawDamage)
    {
        Attacker = attacker;
        Target = target;
        RawDamage = rawDamage;
    }
}