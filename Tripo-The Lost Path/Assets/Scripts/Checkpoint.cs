using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool activateOnce = true;

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (activated && activateOnce) return;

        activated = true;

        CheckpointManager.Instance.SetCheckpoint(transform);

        OnCheckpointReached();
    }

    private void OnCheckpointReached()
    {
        // Optional: VFX, SFX, UI text
        Debug.Log("Checkpoint Reached!");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
