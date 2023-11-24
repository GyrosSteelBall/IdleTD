public interface IGameState
{
    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();
}