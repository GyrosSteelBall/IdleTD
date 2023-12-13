using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyController Controller { get; private set; }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
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
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    // Add other animation methods as needed
}