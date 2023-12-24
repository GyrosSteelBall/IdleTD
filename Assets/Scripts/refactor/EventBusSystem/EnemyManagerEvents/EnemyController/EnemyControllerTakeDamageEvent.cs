public class EnemyControllerTakeDamageEvent
{
    public EnemyController Emitter { get; private set; }
    public int Damage { get; private set; }

    public EnemyControllerTakeDamageEvent(EnemyController emitter, int damage)
    {
        Emitter = emitter;
        Damage = damage;
    }
}