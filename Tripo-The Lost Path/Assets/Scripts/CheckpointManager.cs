using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Transform currentCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
        Debug.Log($"Checkpoint set: {checkpoint.name}");
    }

    public Vector3 GetRespawnPosition()
    {
        if (currentCheckpoint != null)
            return currentCheckpoint.position;

        // Fallback (start of level)
        return Vector3.zero;
    }

    public bool HasCheckpoint()
    {
        return currentCheckpoint != null;
    }
}
