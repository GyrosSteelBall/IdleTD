using UnityEngine;

public class EnemyControllerMovementDirectionChangedEvent
{
    public EnemyController Emitter { get; private set; }
    public string Direction { get; private set; }

    public EnemyControllerMovementDirectionChangedEvent(EnemyController emitter, string direction)
    {
        Emitter = emitter;
        Direction = direction;
    }
}