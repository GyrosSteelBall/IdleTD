public interface IGameState
{
    void OnStateEnter();
    void OnStateExit();
    void Update();
}