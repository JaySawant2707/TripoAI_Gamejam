using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform targetPos;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        other.transform.position = targetPos.position;

        if (controller != null)
            controller.enabled = true;

        Debug.Log("Teleported");
    }
}
