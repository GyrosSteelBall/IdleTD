public class UnitControllerChangeLookDirectionEvent
{
    public UnitController Emitter { get; set; }
    public string LookDirection { get; set; }

    public UnitControllerChangeLookDirectionEvent(UnitController unitController, string lookDirection)
    {
        Emitter = unitController;
        LookDirection = lookDirection;
    }
}