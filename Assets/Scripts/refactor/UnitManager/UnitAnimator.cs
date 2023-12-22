using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    public UnitController Controller { get; private set; }

    public void Initialize(UnitController controller)
    {
        Controller = controller;
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<UnitControllerIdleStateEvent>(OnUnitControllerIdleStateEvent);
        EventBus.Instance.Subscribe<UnitControllerChangeLookDirectionEvent>(OnUnitControllerChangeLookDirectionEvent);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UnitControllerIdleStateEvent>(OnUnitControllerIdleStateEvent);
        EventBus.Instance.Unsubscribe<UnitControllerChangeLookDirectionEvent>(OnUnitControllerChangeLookDirectionEvent);
    }

    private void OnUnitControllerChangeLookDirectionEvent(UnitControllerChangeLookDirectionEvent lookDirectionEvent)
    {
        if (lookDirectionEvent.Emitter != Controller)
        {
            return;
        }

        //Just flip the sprite for now
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = lookDirectionEvent.LookDirection == "left";
    }

    private void OnUnitControllerIdleStateEvent(UnitControllerIdleStateEvent idleStateEvent)
    {
        if (idleStateEvent.Emitter != Controller)
        {
            return;
        }

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    public void SetController(UnitController controller)
    {
        Controller = controller;
    }

    // Add other animation methods as needed
}