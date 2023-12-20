using UnityEngine;

public class UnitIdleState : IUnitState
{
    private UnitController emitter;

    public UnitIdleState(UnitController unit)
    {
        this.emitter = unit;
    }

    public void Enter()
    {
        EventBus.Instance.Publish(new UnitControllerIdleStateEvent(emitter));
    }

    public void Update()
    {
        // Check if enemies are in range circle
        // If so, change state to attack
    }

    public void Exit()
    {
        // Code to execute when exiting the idle state
    }
}