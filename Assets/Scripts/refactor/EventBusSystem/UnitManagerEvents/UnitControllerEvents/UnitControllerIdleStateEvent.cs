using System;

public class UnitControllerIdleStateEvent
{
    public UnitController Emitter { get; private set; }

    public UnitControllerIdleStateEvent(UnitController emitter)
    {
        Emitter = emitter;
    }
}
