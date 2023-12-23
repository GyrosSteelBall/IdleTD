public class CombatSystemApplyDamageToEnemyEvent
{
    public EnemyController Target { get; set; }
    public int Damage { get; set; }

    public CombatSystemApplyDamageToEnemyEvent(EnemyController target, int damage)
    {
        Target = target;
        Damage = damage;
    }
}