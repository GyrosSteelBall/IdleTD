using System;

public class GameManager : Singleton<GameManager>
{
    private IGameState _currentState;
    // Event to notify when the game state changes
    public event Action<IGameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();
        ChangeState(new PreparationState());
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.OnStateExit();
        _currentState = newState;
        OnGameStateChanged?.Invoke(_currentState);
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        _currentState?.Update();
    }

    protected override void OnDestroy()
    {
        // This will handle the singleton destruction.
        base.OnDestroy();
        OnGameStateChanged = null; // Unsubscribe all listeners to prevent memory leaks
    }
}