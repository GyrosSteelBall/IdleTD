public class GameEvent { }

public class EnemyDeathEvent : GameEvent
{
    public EnemyController Enemy { get; private set; }

    public EnemyDeathEvent(EnemyController enemy)
    {
        Enemy = enemy;
    }
}

public class GameEventManager : Singleton<GameEventManager>
{
    public event Action<GameEvent> OnGameEvent;

    public void Raise(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}