using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 current_position;
    private Quaternion current_rotation;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    [SerializeField]
    [Range(.01f, .1f)]
    private float speed = .05f;

    public void Awake()
    {
        current_position = transform.position;
        current_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
