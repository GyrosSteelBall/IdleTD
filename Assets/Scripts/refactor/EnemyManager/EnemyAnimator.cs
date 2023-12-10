using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    void OnEnable()
    {
        EventBus.Instance.Subscribe<EnemyControllerChangedStateEvent>(HandleChangeState);
    }

    void OnDisable()
    {
        EventBus.Instance.Unsubscribe<EnemyControllerChangedStateEvent>(HandleChangeState);
    }

    private void HandleChangeState(EnemyControllerChangedStateEvent changeStateEvent)
    {
        switch (changeStateEvent.NewState)
        {
            case EnemyMovingState movingState:
                AnimateMoving();
                break;
            // Add cases for other states as needed
            default:
                Debug.LogWarning("Unhandled state in EnemyAnimator");
                break;
        }
    }

    private void AnimateMoving()
    {
        // Assuming you have an Animator component attached to the same GameObject
        Animator animator = GetComponent<Animator>();

        // Trigger the "Moving" animation state
        animator.SetTrigger("Moving");
    }

    // Add other animation methods as needed
}