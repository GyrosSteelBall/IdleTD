public class ResourceManager : Singleton<ResourceManager>
{
    // Resource manager specific code...

    protected override void Awake()
    {
        base.Awake();
        GameEventManager.Instance.OnGameEvent += HandleGameEvent;
    }

    private void HandleGameEvent(GameEvent gameEvent)
    {
        if (gameEvent is EnemyDeathEvent enemyDeathEvent)
        {
            // Increase resources when an enemy dies
            // ...
        }
    }

    private void OnDestroy()
    {
        if (GameEventManager.Instance != null)
        {
            GameEventManager.Instance.OnGameEvent -= HandleGameEvent;
        }
    }
}