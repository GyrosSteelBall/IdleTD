using System;
using System.Collections;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    public UnitController Controller { get; private set; }
    //Maybe move into a separate class
    private static Material DefaultMaterial;
    private static Material BlinkMaterial;

    public void Initialize(UnitController controller)
    {
        Controller = controller;
    }

    void OnEnable()
    {
        EventBus.Instance.Subscribe<UnitControllerIdleStateEvent>(OnUnitControllerIdleStateEvent);
        EventBus.Instance.Subscribe<UnitControllerTakeDamageEvent>(OnUnitControllerTakeDamageEvent);
        EventBus.Instance.Subscribe<UnitControllerChangeLookDirectionEvent>(OnUnitControllerChangeLookDirectionEvent);
        EventBus.Instance.Subscribe<UnitControllerAttackEvent>(OnUnitControllerAttackEvent);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UnitControllerIdleStateEvent>(OnUnitControllerIdleStateEvent);
        EventBus.Instance.Unsubscribe<UnitControllerTakeDamageEvent>(OnUnitControllerTakeDamageEvent);
        EventBus.Instance.Unsubscribe<UnitControllerChangeLookDirectionEvent>(OnUnitControllerChangeLookDirectionEvent);
        EventBus.Instance.Unsubscribe<UnitControllerAttackEvent>(OnUnitControllerAttackEvent);
    }

    private void OnUnitControllerAttackEvent(UnitControllerAttackEvent attackEvent)
    {
        if (attackEvent.Attacker != Controller)
        {
            return;
        }

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Attack");
    }

    private void OnUnitControllerTakeDamageEvent(UnitControllerTakeDamageEvent takeDamageEvent)
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

    private IEnumerator BlinkCoroutine(Renderer renderer)
    {
        renderer.material = BlinkMaterial;

        yield return new WaitForSeconds(0.1f);

        renderer.material = DefaultMaterial;
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