public class Enemy
{
    public EnemyController Controller { get; private set; }

    // Add other components as needed

    public Enemy(EnemyController controller)
    {
        Controller = controller;
    }
}