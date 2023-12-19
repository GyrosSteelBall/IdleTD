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
        // Code to execute on each update while in the idle state
        // For example, you might want to check if there are any enemies in range, and if so, transition to the attacking state
    }

    public void Exit()
    {
        // Code to execute when exiting the idle state
    }
}