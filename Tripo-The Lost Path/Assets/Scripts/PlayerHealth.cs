using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] AudioClip deathSound;
    public int CurrentHealth { get; private set; }

    public bool IsDead { get; private set; }

    // Optional events (great for UI, sounds, effects)
    public event Action<int, int> OnHealthChanged; // current, max
    public event Action OnPlayerDied;

    private float playerPosAtDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    void Update()
    {
        if (IsDead)
        {
            LowerPlayer();
        }
    }

    // -------------------------
    // Damage
    // -------------------------
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        amount = Mathf.Abs(amount);
        CurrentHealth -= amount;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            KillPlayer();
        }
    }

    // -------------------------
    // Heal
    // -------------------------
    public void Heal(int amount)
    {
        if (IsDead) return;

        amount = Mathf.Abs(amount);
        CurrentHealth += amount;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    // -------------------------
    // Kill Player
    // -------------------------
    public void KillPlayer()
    {
        if (IsDead) return;

        IsDead = true;
        CurrentHealth = 0;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        Debug.Log("Player Died");

        OnPlayerDied?.Invoke();

        TogglePlayerControl(false);

        var ani = GetComponent<Animator>();
        if (ani != null) ani.SetTrigger("Death");

        playerPosAtDeath = transform.position.y - 0.45f;

        if (deathSound) AudioSource.PlayClipAtPoint(deathSound, transform.position);

        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        Vector3 respawnPos = CheckpointManager.Instance.HasCheckpoint()
            ? CheckpointManager.Instance.GetRespawnPosition()
            : transform.position;

        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        TogglePlayerControl(true);

        transform.position = respawnPos;

        if (controller != null)
            controller.enabled = true;

        CurrentHealth = maxHealth;
        IsDead = false;

        Debug.Log("Player Respawned");
    }

    void LowerPlayer()
    {
        float currentValue = Mathf.Lerp(transform.position.y, playerPosAtDeath, 1 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, currentValue, transform.position.z);
    }

    private void TogglePlayerControl(bool state)
    {
        // Disable Starter Assets controller
        var controller = GetComponent<StarterAssets.ThirdPersonController>();
        if (controller != null)
            controller.enabled = state;

        // Optional: disable input
        var input = GetComponent<StarterAssets.StarterAssetsInputs>();
        if (input != null)
            input.enabled = state;
    }
}
