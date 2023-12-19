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
}

public interface IUnitState
{
    void Enter();
    void Update();
    void Exit();
}
