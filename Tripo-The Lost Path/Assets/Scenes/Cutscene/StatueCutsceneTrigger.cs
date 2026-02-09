using UnityEngine;

public class StatueCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private StatueCutsceneController cutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        cutscene.Play();
        gameObject.SetActive(false); // one-time trigger
    }
}
