public class EnemyMovingState : IEnemyState
{
    public void OnStateEnter(EnemyController enemyController)
    {
        // Initialize moving state (e.g., set a target destination)
    }

    public void Update(EnemyController enemyController)
    {
        // Implement the logic for moving the enemy
        enemyController.MoveTowardsNextWaypoint();
    }

    public void OnStateExit(EnemyController enemyController)
    {
        // Cleanup or final actions when leaving the moving state
    }
}
