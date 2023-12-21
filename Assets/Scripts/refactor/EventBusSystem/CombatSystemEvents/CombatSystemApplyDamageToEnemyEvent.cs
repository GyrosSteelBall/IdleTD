public class CombatSystemApplyDamageToEnemyEvent
{
    public EnemyController Target { get; set; }
    public float Damage { get; set; }

    public CombatSystemApplyDamageToEnemyEvent(EnemyController target, float damage)
    {
        Target = target;
        Damage = damage;
    }
}