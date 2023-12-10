public class EnemyFactoryEnemySpawnedEvent
{
    public Enemy Enemy { get; private set; }
    public EnemyFactoryEnemySpawnedEvent(Enemy enemy)
    {
        Enemy = enemy;
    }
}