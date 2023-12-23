public class EnemyControllerStateChangedEvent
{
    public EnemyController Emitter { get; set; }
    public IEnemyState NewState { get; set; }

    public EnemyControllerStateChangedEvent(EnemyController enemyController, IEnemyState newState)
    {
        Emitter = enemyController;
        NewState = newState;
    }
}