public class UnitControllerDeathEvent
{
    public UnitController UnitController { get; private set; }

    public UnitControllerDeathEvent(UnitController unitController)
    {
        UnitController = unitController;
    }
}