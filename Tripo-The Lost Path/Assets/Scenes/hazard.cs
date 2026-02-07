using UnityEngine;

public class DeadVolume : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 50f;
    public bool instantKill = false;

    [Header("Respawn Settings")]
    public Transform respawnPoint;
    public string playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        Health health = other.GetComponent<Health>();
        if (health == null) return;

        if (instantKill)
        {
            health.TakeDamage(health.currentHealth);
            Respawn(other.gameObject);
        }
        else
        {
            health.TakeDamage(damagePerSecond * Time.deltaTime);

            if (health.currentHealth <= 0)
            {
                Respawn(other.gameObject);
            }
        }
    }

    void Respawn(GameObject player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;

        if (controller != null)
            controller.enabled = true;

        // Reset health after respawn
        Health health = player.GetComponent<Health>();
        if (health != null)
            health.currentHealth = health.maxHealth;
    }
}