using UnityEngine;
using StarterAssets;
using UnityEngine.Playables;

public class StatueCutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;

    private ThirdPersonController playerController;
    private StarterAssetsInputs playerInput;

    private void Awake()
    {
        playerController = FindFirstObjectByType<ThirdPersonController>();
        playerInput = FindFirstObjectByType<StarterAssetsInputs>();
    }

    public void Play()
    {
        if (playerController) playerController.enabled = false;
        if (playerInput) playerInput.enabled = false;

        timeline.Play();
    }

    // Called from Timeline Signal
    public void EndCutscene()
    {
        if (playerController) playerController.enabled = true;
        if (playerInput) playerInput.enabled = true;
    }
}
