public class EnemyControllerReachedFinalWaypointEvent
{
    public EnemyController EnemyController { get; private set; }

    public EnemyControllerReachedFinalWaypointEvent(EnemyController enemyController)
    {
        EnemyController = enemyController;
    }
}