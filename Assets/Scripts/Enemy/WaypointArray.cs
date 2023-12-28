using UnityEngine;

public class WaypointArray : MonoBehaviour
{
    public Transform[] waypoints; // Array to store the child waypoints

    void Start()
    {
        // Find all child waypoints and store their Transform components in the array
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
