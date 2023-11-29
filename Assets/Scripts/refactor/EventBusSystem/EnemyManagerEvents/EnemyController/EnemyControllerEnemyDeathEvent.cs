public class EnemyControllerEnemyDeathEvent
{
    public EnemyController EnemyController { get; private set; }

    public EnemyControllerEnemyDeathEvent(EnemyController enemyController)
    {
        EnemyController = enemyController;
    }
}
