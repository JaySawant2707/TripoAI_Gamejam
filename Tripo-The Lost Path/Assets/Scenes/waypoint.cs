using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [Header("Waypoint Settings")]
    public Transform[] waypoints;
    public float speed = 3f;
    public float reachDistance = 0.2f;

    [Header("Loop Settings")]
    public bool loop = true;

    [Header("Rotation Settings")]
    public bool faceForward = true;
    public float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        MoveToWaypoint();
        RotateTowardsWaypoint();
    }

    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];

        // Move towards waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, target.position) <= reachDistance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loop)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }

    void RotateTowardsWaypoint()
    {
        if (!faceForward) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;

        // Ignore Y rotation if needed (for ground characters)
        direction.y = 0f;

        if (direction.magnitude == 0) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}