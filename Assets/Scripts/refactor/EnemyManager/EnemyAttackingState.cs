using UnityEngine;
public class EnemyAttackingState : IEnemyState
{
    private UnitController target;
    private float nextAttackTime;

    public EnemyAttackingState(UnitController target)
    {
        this.target = target;
        nextAttackTime = 0f;
    }

    public void OnStateEnter(EnemyController enemyController)
    {
        // Initialize attacking state (e.g., play attack animation)
    }

    public void Update(EnemyController enemyController)
    {
        // Implement the logic for attacking the target
        if (Time.time >= nextAttackTime)
        {
            enemyController.Attack(target);
            nextAttackTime = Time.time + 1f / enemyController.GetAttackSpeed();
        }
    }

    public void OnStateExit(EnemyController enemyController)
    {
        // Cleanup or final actions when leaving the attacking state
    }
}