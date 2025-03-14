using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager Instance;
    public List<Transform> waypoints = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Transform GetClosestWaypoint(Vector3 position)
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(position, waypoint.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = waypoint;
            }
        }

        return closest;
    }
}
