public class LossGameState : IGameState
{
    public void OnStateEnter()
    {
        // Display the loss message on the UI
    }

    public void OnStateExit()
    {
        // Clean up before transitioning to the next state
    }

    public void Update()
    {
        // Ongoing checks during the state, e.g., user input for restarting the game
    }
}