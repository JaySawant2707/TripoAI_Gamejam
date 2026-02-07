using UnityEngine;
using DG.Tweening;
using StarterAssets;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup backgroundDim;
    [SerializeField] private RectTransform pausePanel;

    [Header("Animation")]
    [SerializeField] private float fadeDuration = 0.2f;
    [SerializeField] private float scaleDuration = 0.2f;

    [Header("Player")]
    [SerializeField] GameObject playerObject;

    private bool isPaused;

    private ThirdPersonController playerController;
    private StarterAssetsInputs playerInput;

    private void OnEnable()
    {
        if (Time.timeScale == 1f)
            LockCursor();
    }

    private void Awake()
    {
        playerController = playerObject.GetComponent<ThirdPersonController>();
        playerInput = playerObject.GetComponent<StarterAssetsInputs>();

        gameObject.SetActive(true);
        backgroundDim.alpha = 0f;
        pausePanel.localScale = Vector3.one * 0.9f;
        pausePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        // ESC key (New Input System safe)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;

        Time.timeScale = 0f;

        if (playerController != null)
            playerController.enabled = false;

        if (playerInput != null)
            playerInput.enabled = false;

        UnlockCursor(); // ⭐ THIS FIXES IT

        pausePanel.gameObject.SetActive(true);

        backgroundDim.DOKill();

        backgroundDim
            .DOFade(0.6f, fadeDuration)
            .SetUpdate(true);

        backgroundDim.DOKill();

        pausePanel
            .DOScale(1f, scaleDuration)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true);
    }


    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;

        backgroundDim.DOKill();

        backgroundDim
            .DOFade(0f, fadeDuration)
            .SetUpdate(true);

        pausePanel.DOKill();

        pausePanel
            .DOScale(0.9f, scaleDuration)
            .SetEase(Ease.InCubic)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                pausePanel.gameObject.SetActive(false);

                Time.timeScale = 1f;

                if (playerController != null)
                    playerController.enabled = true;

                if (playerInput != null)
                    playerInput.enabled = true;

                LockCursor(); // ⭐ RESTORE GAME STATE
            });
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerInput != null)
            playerInput.cursorLocked = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (playerInput != null)
            playerInput.cursorLocked = true;
    }

    // ---------- Button Hooks ----------
    public void OnResumePressed() => Resume();

    public void OnRestartPressed()
    {
        UnlockCursor();
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
