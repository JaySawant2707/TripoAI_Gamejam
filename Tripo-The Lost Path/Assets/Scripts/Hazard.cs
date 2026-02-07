using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damageAmount = 10;

    [Tooltip("If true, damage is applied every interval while inside trigger")]
    [SerializeField] private bool continuousDamage = false;

    [Tooltip("Time between damage ticks (used only if continuousDamage is true)")]
    [SerializeField] private float damageInterval = 1f;

    private float damageTimer;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        // Instant damage (spikes, bullets, traps)
        if (!continuousDamage)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!continuousDamage) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null || playerHealth.IsDead) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            damageTimer = 0f;
            playerHealth.TakeDamage(damageAmount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset timer when player leaves
        if (other.GetComponent<PlayerHealth>())
        {
            damageTimer = 0f;
        }
    }
}
