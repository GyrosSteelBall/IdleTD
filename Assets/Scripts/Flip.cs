using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour { 

    private float moveInput;
    public float moveSpeed;
    Rigidbody2D rb2d;

    float scaleX;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        FlipCharacter();
    }
    
    public void FlipCharacter()
    {
        transform.localScale = new Vector3(scaleX * (-1), transform.localScale.y, transform.localScale.z);
    }
}
