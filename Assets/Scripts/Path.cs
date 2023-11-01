using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform GetNextWaypoint(int currentIndex)
    {
        if (currentIndex < waypoints.Length - 1)
        {
            return waypoints[currentIndex + 1];
        }
        else
        {
            return null;
        }
    }
}
