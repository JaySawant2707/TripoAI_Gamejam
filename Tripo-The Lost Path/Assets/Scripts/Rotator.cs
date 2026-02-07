using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation axis direction")]
    public Vector3 rotationAxis = Vector3.up;

    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 90f;

    [Tooltip("Rotate in world space instead of local space")]
    public bool useWorldSpace = false;

    [Tooltip("Enable or disable rotation")]
    public bool rotate = true;

    void Update()
    {
        if (!rotate || rotationAxis == Vector3.zero)
            return;

        float angle = rotationSpeed * Time.deltaTime;

        if (useWorldSpace)
        {
            transform.Rotate(rotationAxis.normalized, angle, Space.World);
        }
        else
        {
            transform.Rotate(rotationAxis.normalized, angle, Space.Self);
        }
    }

    // Optional runtime controls
    public void SetSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }

    public void SetAxis(Vector3 axis)
    {
        rotationAxis = axis;
    }

    public void EnableRotation(bool value)
    {
        rotate = value;
    }
}