public class UnitControllerAttackEvent
{
    public UnitController Attacker { get; set; }
    public EnemyController Target { get; set; }
    public float RawDamage { get; set; }

    public UnitControllerAttackEvent(UnitController attacker, EnemyController target, float rawDamage)
    {
        Attacker = attacker;
        Target = target;
        RawDamage = rawDamage;
    }
}