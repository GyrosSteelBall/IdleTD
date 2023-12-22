using UnityEngine;

public class UnitCombatState : IUnitState
{
    private UnitController unitController;
    private EnemyController target;

    public UnitCombatState(UnitController unit, EnemyController target)
    {
        this.unitController = unit;
        this.target = target;
    }

    public void Enter()
    {
        // EventBus.Instance.Publish(new UnitControllerCombatStateEvent(emitter));
    }

    public void Update()
    {
        // Get all enemies in the scene
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

        // Reset the target
        this.target = null;

        foreach (var enemy in enemies)
        {
            // Calculate the distance between the unit and the enemy in the XY plane
            Vector2 unitPosition = new Vector2(unitController.transform.position.x, unitController.transform.position.y);
            Vector2 enemyPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            float distance = Vector2.Distance(unitPosition, enemyPosition);

            // If the enemy is within attack range, set the enemy as the target
            if (distance <= unitController.Unit.AttackRange)
            {
                target = enemy;
                break;
            }
        }

        // If there is a target, attack the target
        if (target != null)
        {
            unitController.Attack(target);
            // Change the direction the unit is facing
            Vector3 directionVector = (target.transform.position - unitController.transform.position).normalized;
            string direction;
            direction = directionVector.x > 0 ? "right" : "left";
            EventBus.Instance.Publish(new UnitControllerChangeLookDirectionEvent(unitController, direction));
        }
        else
        {
            // If there is no target, change the state back to idle
            unitController.ChangeState(new UnitIdleState(unitController));
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the idle state
    }
}