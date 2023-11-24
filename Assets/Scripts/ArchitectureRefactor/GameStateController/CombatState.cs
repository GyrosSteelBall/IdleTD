public class CombatState : IGameState
{
    private readonly GameStateController controller;

    // References to systems mentioned in architecture
    // private WaveManager waveManager;
    // private CombatHandler combatHandler;
    // private InteractionController interactionController;

    public CombatState(GameStateController controller)
    {
        this.controller = controller;
        // initialization of systems
    }

    public void OnStateEnter()
    {
        // Start the wave manager
        // waveManager.StartWaves();

        // Enable player interactions
        // interactionController.Enable();
    }

    public void OnStateUpdate()
    {
        // Update the wave progress and check win/lose conditions
        // combatHandler.ProcessCombat();

        // Transition to other states based on conditions
        // if (waveManager.IsVictorious)
        // {
        //     controller.SetState(new VictoryState(controller));
        // }
        // else if (waveManager.IsDefeated)
        // {
        //     controller.SetState(new DefeatState(controller));
        // }
        // else if (Input.GetKeyDown(KeyCode.Escape)) // Pause condition, as an example
        // {
        //     controller.SetState(new PauseState(controller));
        // }
    }

    public void OnStateExit()
    {
        // Handle cleanup (if any) before transitioning to a new state
        // interactionController.Disable();
    }
}