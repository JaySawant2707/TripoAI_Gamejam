using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 2;
    [SerializeField] private float skipDelay = 0.5f;

    private VideoPlayer videoPlayer;
    private bool canSkip;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Start()
    {
        videoPlayer.Play();
        Invoke(nameof(EnableSkip), skipDelay);
    }

    private void EnableSkip()
    {
        canSkip = true;
    }

    private void Update()
    {
        if (!canSkip) return;

        // Skip on any key / mouse / gamepad button
        if (Keyboard.current.anyKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            SkipCutscene();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    private void SkipCutscene()
    {
        videoPlayer.Stop();
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
