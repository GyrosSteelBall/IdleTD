using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedComponent : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}
