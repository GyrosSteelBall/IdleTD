public class EnemyControllerAttackEvent
{
    public EnemyController Attacker { get; set; }
    public UnitController Target { get; set; }
    public float RawDamage { get; set; }

    public EnemyControllerAttackEvent(EnemyController attacker, UnitController target, float rawDamage)
    {
        Attacker = attacker;
        Target = target;
        RawDamage = rawDamage;
    }
}