public interface IEnemyState
{
    void OnStateEnter(EnemyController enemyController);
    void Update(EnemyController enemyController);
    void OnStateExit(EnemyController enemyController);
}
