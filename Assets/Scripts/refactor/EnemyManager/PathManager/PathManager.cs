using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{

    [SerializeField]
    private List<Path> paths = new List<Path>();

    public List<Vector3> GetPath(int pathIndex)
    {
        List<Vector3> waypoints = new List<Vector3>();
        if (pathIndex >= 0 && pathIndex < paths.Count)
        {
            foreach (Transform waypoint in paths[pathIndex].waypoints)
            {
                if (waypoint != null)
                {
                    waypoints.Add(waypoint.position);
                }
            }
        }
        return waypoints; // Return the list of waypoint positions
    }

    // You can add methods to load paths or define them in the editor.
}
