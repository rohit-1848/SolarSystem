using UnityEngine;

public class TrolleyPathFollower : MonoBehaviour
{
    [Header("Track Setup")]
    public Transform[] waypoints; // Drag your invisible track points here
    public float speed = 3f;      // How fast the trolley moves
    
    [Header("Movement Style")]
    public bool lookForward = true; // Should the trolley turn to face the track?
    public float turnSpeed = 5f;    // How fast it corners
    public bool loopPath = true;    // Does it go back to start when finished?

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Stop if there are no waypoints assigned
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];

        // 1. Move the trolley towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 2. Rotate the trolley so it faces where it is going
        if (lookForward)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
        }

        // 3. Check if we have reached the waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next target

            // Handle what happens at the end of the track
            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loopPath)
                {
                    currentWaypointIndex = 0; // Loop back to the first point
                }
                else
                {
                    currentWaypointIndex = waypoints.Length - 1; // Stop at the end
                }
            }
        }
    }
}