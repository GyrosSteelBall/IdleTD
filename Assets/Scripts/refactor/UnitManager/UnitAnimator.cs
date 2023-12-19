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
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UnitControllerIdleStateEvent>(OnUnitControllerIdleStateEvent);
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