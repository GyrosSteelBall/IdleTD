using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy.MovementSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.Move(new Vector3(1, 2, 0));
    }
}
