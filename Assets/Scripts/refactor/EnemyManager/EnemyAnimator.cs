using System;
using UnityEngine;

// TODO: refactor this to not get animator component every time
public class EnemyAnimator : MonoBehaviour
{
    public EnemyController Controller { get; private set; }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
        EventBus.Instance.Subscribe<EnemyControllerAttackEvent>(HandleAttack);
        EventBus.Instance.Subscribe<EnemyControllerStateChangedEvent>(HandleStateChanged);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
        EventBus.Instance.Unsubscribe<EnemyControllerAttackEvent>(HandleAttack);
        EventBus.Instance.Unsubscribe<EnemyControllerStateChangedEvent>(HandleStateChanged);
    }

    private void HandleStateChanged(EnemyControllerStateChangedEvent stateChangedEvent)
    {
        if (stateChangedEvent.Emitter != Controller)
        {
            return;
        }
        IEnemyState state = stateChangedEvent.NewState;
        Animator animator = GetComponent<Animator>();
        if (state is EnemyAttackingState)
        {
            animator.SetBool("Moving", false);
        }
        else if (state is EnemyMovingState)
        {
            animator.SetBool("Moving", true);
        }
    }

    private void HandleAttack(EnemyControllerAttackEvent attackEvent)
    {
        if (attackEvent.Attacker != Controller)
        {
            return;
        }

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Attack");
    }

    public void SetController(EnemyController controller)
    {
        Controller = controller;
    }

    private void HandleMoving(EnemyControllerMovementDirectionChangedEvent movingEvent)
    {
        if (movingEvent.Emitter != Controller)
        {
            return;
        }
        string direction = movingEvent.Direction;
        Animator animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        if (direction == "up")
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 1);
        }
        else if (direction == "down")
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", -1);
        }
        else if (direction == "left")
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", -1);
            animator.SetFloat("MoveY", 0);
            spriteRenderer.flipX = true; // Flip the sprite when moving left
        }
        else if (direction == "right")
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", 1);
            animator.SetFloat("MoveY", 0);
            spriteRenderer.flipX = false; // Don't flip the sprite when moving right
        }
    }

    // Add other animation methods as needed
}