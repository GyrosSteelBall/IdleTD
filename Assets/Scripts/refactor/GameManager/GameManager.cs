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

    void OnEnable()
    {
        EventBus.Instance.Subscribe<LivesManagerLivesDepletedEvent>(HandleLivesDepleted);
        EventBus.Instance.Subscribe<WaveManagerWaveStartedEvent>(HandleWaveStarting);
        EventBus.Instance.Subscribe<WaveManagerWaveCompletedEvent>(HandleWaveCompletion);
    }

    void OnDisable()
    {
        EventBus.Instance.Subscribe<LivesManagerLivesDepletedEvent>(HandleLivesDepleted);
        EventBus.Instance.Unsubscribe<WaveManagerWaveStartedEvent>(HandleWaveStarting);
        EventBus.Instance.Unsubscribe<WaveManagerWaveCompletedEvent>(HandleWaveCompletion);
    }

    private void HandleLivesDepleted(LivesManagerLivesDepletedEvent inputEvent)
    {
        ChangeState(new LossGameState());
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

    private void HandleWaveStarting(WaveManagerWaveStartedEvent inputEvent)
    {
        ChangeState(new WaveInProgressState());
    }

    private void HandleWaveCompletion(WaveManagerWaveCompletedEvent inputEvent)
    {
        // Change the state to WaveCompleted for any inter-wave logic or wait
        ChangeState(new WaveCompletedState());
        // Possibly start a countdown for the next wave, prepare for the next state, or simply go to the next wave immediately
    }

    protected override void OnDestroy()
    {
        // This will handle the singleton destruction.
        base.OnDestroy();
    }
}