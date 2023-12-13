public class Enemy
{
    public EnemyController Controller { get; private set; }
    public EnemyAnimator Animator { get; private set; }

    public Enemy(EnemyController controller, EnemyAnimator animator)
    {
        Controller = controller;
        Animator = animator;
    }
}