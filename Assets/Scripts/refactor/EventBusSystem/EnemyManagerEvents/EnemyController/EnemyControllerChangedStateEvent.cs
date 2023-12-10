using System;

public class EnemyControllerChangedStateEvent
{
    public IEnemyState NewState { get; }

    public EnemyControllerChangedStateEvent(IEnemyState newState)
    {
        NewState = newState;
    }
}