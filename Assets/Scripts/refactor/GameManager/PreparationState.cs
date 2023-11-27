public class PreparationState : IGameState
{
    public void OnStateEnter()
    {
        // Preparations for the state, e.g., enabling the UI for unit placement
    }

    public void OnStateExit()
    {
        // Clean up before transitioning to the next state
    }

    public void Update()
    {
        // Ongoing checks during the state, e.g., user input for unit placement
    }
}

// Implement other states similarly (WaveInProgressState, WaveCompletedState, etc.)