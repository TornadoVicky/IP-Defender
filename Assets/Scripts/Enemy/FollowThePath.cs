using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    private int waypointIndex = 0;
    private Animator animator; // Reference to the Animator component

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;

                // Check if the enemy has reached the last waypoint
                if (waypointIndex == waypoints.Length)
                {
                    // Start the "Attack" animation
                    if (animator != null)
                    {
                        animator.SetTrigger("Attack");
                    }
                }
            }
        }
    }
}
