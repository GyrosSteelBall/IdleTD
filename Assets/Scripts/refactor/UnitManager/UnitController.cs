using UnityEngine;

public class UnitController : MonoBehaviour
{
    private IUnitState currentState;
    public IUnit Unit { get; private set; }

    public void Initialize(IUnit unit)
    {
        Unit = unit;
    }

    //Initial state is idle
    public void Start()
    {
        currentState = new UnitIdleState(this);
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IUnitState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void OnDrawGizmos()
    {
        if (currentState is UnitCombatState)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, Unit.AttackRange);
    }

    public void Attack(EnemyController enemy)
    {
        float damage = Unit.AttackDamage;
        EventBus.Instance.Publish(new UnitControllerAttackEvent(this, enemy, damage));
    }
}

public interface IUnitState
{
    void Enter();
    void Update();
    void Exit();
}
