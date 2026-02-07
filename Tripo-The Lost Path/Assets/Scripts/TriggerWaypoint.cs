using UnityEngine;

public class TriggerWaypoint : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] WaypointMover waypointMover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && waypointMover != null)
        {
            waypointMover.gameObject.SetActive(true);
            waypointMover.CanMove(true);
        }
    }
}