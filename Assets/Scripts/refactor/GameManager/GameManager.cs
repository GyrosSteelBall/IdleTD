using System;

public class GameManager : Singleton<GameManager>
{
    private IGameState _currentState;
    // Event to notify when the game state changes
    public event Action<IGameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();
        WaveManager.Instance.OnWaveStarted += HandleWaveStarting;
        WaveManager.Instance.OnWaveCompleted += HandleWaveCompletion;
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

    private void HandleWaveStarting(int waveNumber)
    {
        ChangeState(new WaveInProgressState());
    }

    private void HandleWaveCompletion()
    {
        // Change the state to WaveCompleted for any inter-wave logic or wait
        ChangeState(new WaveCompletedState());
        // Possibly start a countdown for the next wave, prepare for the next state, or simply go to the next wave immediately
    }

    protected override void OnDestroy()
    {
        // This will handle the singleton destruction.
        base.OnDestroy();
        WaveManager.Instance.OnWaveCompleted -= HandleWaveCompletion;
        WaveManager.Instance.OnWaveStarted -= HandleWaveStarting;
        OnGameStateChanged = null; // Unsubscribe all listeners to prevent memory leaks
    }
}