using UnityEngine;
public class EnemyAttackingState : IEnemyState
{
    private UnitController target;
    private float nextAttackTime;
    private EnemyController enemyController;

    public EnemyAttackingState(UnitController target)
    {
        this.target = target;
        nextAttackTime = 0f;
    }

    public void OnStateEnter(EnemyController enemyController)
    {
        EventBus.Instance.Subscribe<UnitControllerDeathEvent>(HandleTargetDeath);
        EventBus.Instance.Publish(new EnemyControllerStateChangedEvent(enemyController, this));
        this.enemyController = enemyController;
    }

    private void HandleTargetDeath(UnitControllerDeathEvent unitDeathEvent)
    {
        if (unitDeathEvent.UnitController == target)
        {
            this.enemyController.ChangeState(new EnemyMovingState());
        }
    }

    public void Update(EnemyController enemyController)
    {
        if (Time.time >= nextAttackTime)
        {
            enemyController.Attack(target);
            nextAttackTime = Time.time + 1f / enemyController.GetAttackSpeed();
        }
    }

    public void OnStateExit(EnemyController enemyController)
    {
        // Cleanup or final actions when leaving the attacking state
        EventBus.Instance.Unsubscribe<UnitControllerDeathEvent>(HandleTargetDeath);
    }
}