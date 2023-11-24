public class DefeatState : IGameState
{
    private readonly GameStateController controller;

    public DefeatState(GameStateController controller)
    {
        this.controller = controller;
    }

    public void OnStateEnter()
    {
        // Display defeat screen with options
        // UIManager.Instance.ShowDefeatScreen();
    }

    public void OnStateUpdate()
    {
        // Wait for player input for retry or exit
        // This can be handled directly here or via UI event listeners
    }

    public void OnStateExit()
    {
        // Hide defeat screen
        // UIManager.Instance.HideDefeatScreen();
    }
}