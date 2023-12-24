using System;
using UnityEngine;
using System.Collections;

// TODO: refactor this to not get animator component every time
public class EnemyAnimator : MonoBehaviour
{
    public EnemyController Controller { get; private set; }
    private static Material DefaultMaterial;
    private static Material BlinkMaterial;

    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
        EventBus.Instance.Subscribe<EnemyControllerAttackEvent>(HandleAttack);
        EventBus.Instance.Subscribe<EnemyControllerStateChangedEvent>(HandleStateChanged);
        EventBus.Instance.Subscribe<EnemyControllerTakeDamageEvent>(HandleTakeDamage);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerMovementDirectionChangedEvent>(HandleMoving);
        EventBus.Instance.Unsubscribe<EnemyControllerAttackEvent>(HandleAttack);
        EventBus.Instance.Unsubscribe<EnemyControllerStateChangedEvent>(HandleStateChanged);
        EventBus.Instance.Unsubscribe<EnemyControllerTakeDamageEvent>(HandleTakeDamage);
    }

    private IEnumerator BlinkCoroutine(Renderer renderer)
    {
        renderer.material = BlinkMaterial;

        yield return new WaitForSeconds(0.1f);

        renderer.material = DefaultMaterial;
    }

    private void HandleTakeDamage(EnemyControllerTakeDamageEvent takeDamageEvent)
    {
        if (takeDamageEvent.Emitter != Controller)
        {
            return;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (DefaultMaterial == null) DefaultMaterial = renderer.material;
        if (BlinkMaterial == null) BlinkMaterial = new Material(Shader.Find("GUI/Text Shader"));

        StartCoroutine(BlinkCoroutine(renderer));
    }

    private void HandleStateChanged(EnemyControllerStateChangedEvent stateChangedEvent)
    {
        if (stateChangedEvent.Emitter != Controller)
        {
            return;
        }
        IEnemyState state = stateChangedEvent.NewState;
        Animator animator = GetComponent<Animator>();
        if (state is EnemyCombatState)
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