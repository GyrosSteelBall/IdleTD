using UnityEngine;

public class UnitIdleState : IUnitState
{
    private UnitController unitController;

    public UnitIdleState(UnitController unit)
    {
        this.unitController = unit;
    }

    public void Enter()
    {
        EventBus.Instance.Publish(new UnitControllerIdleStateEvent(unitController));
    }

    public void Update()
    {
        // Get all enemies in the scene
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

        foreach (var enemy in enemies)
        {
            // Calculate the distance between the unit and the enemy in the XY plane
            Vector2 unitPosition = new Vector2(unitController.transform.position.x, unitController.transform.position.y);
            Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            float distance = Vector2.Distance(unitPosition, enemyPosition);

            // If the enemy is within attack range, change the state to combat and exit the method
            if (distance <= unitController.Unit.AttackRange)
            {
                unitController.ChangeState(new UnitCombatState(unitController, enemy));
                return;
            }
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the idle state
    }
}