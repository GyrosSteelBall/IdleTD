using UnityEngine;

public class UnitController : MonoBehaviour
{
    private IUnitState currentState;
    public IUnit Unit { get; private set; }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<CombatSystemApplyDamageToUnitEvent>(HandleApplyDamageToUnitEvent);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<CombatSystemApplyDamageToUnitEvent>(HandleApplyDamageToUnitEvent);
    }

    private void HandleApplyDamageToUnitEvent(CombatSystemApplyDamageToUnitEvent damageEvent)
    {
        if (damageEvent.Target == this)
        {
            TakeDamage(damageEvent.Damage);
        }
    }

    private void TakeDamage(int damage)
    {
        EventBus.Instance.Publish(new UnitControllerTakeDamageEvent(this, damage));
        Unit.CurrentHealth -= damage;
        if (Unit.CurrentHealth <= 0)
        {
            Die(this);
        }
    }

    private void Die(UnitController unitController)
    {
        EventBus.Instance.Publish(new UnitControllerDeathEvent(unitController));
        Destroy(gameObject);
    }

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
        int damage = Unit.AttackDamage;
        EventBus.Instance.Publish(new UnitControllerAttackEvent(this, enemy, damage));
    }
}

public interface IUnitState
{
    void Enter();
    void Update();
    void Exit();
}
