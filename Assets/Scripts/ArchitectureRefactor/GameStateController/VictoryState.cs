public class VictoryState : IGameState
{
    private readonly GameStateController controller;

    public VictoryState(GameStateController controller)
    {
        this.controller = controller;
    }

    public void OnStateEnter()
    {
        // Display victory screen and stats
        // UIManager.Instance.ShowVictoryScreen();
    }

    public void OnStateUpdate()
    {
        // Wait for player input to either replay, exit, or move to the next level
        // This can be handled directly here or via UI event listeners
    }

    public void OnStateExit()
    {
        // Hide victory screen
        // UIManager.Instance.HideVictoryScreen();
    }
}