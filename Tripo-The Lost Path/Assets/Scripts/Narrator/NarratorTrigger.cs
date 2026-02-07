using UnityEngine;
using System.Collections.Generic;

public class NarratorTrigger : MonoBehaviour
{
    [Header("Narration")]
    public string narrationID;
    public List<NarratorLine> conversation;
    public int priority = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        NarratorManager.Instance.SpeakSequence(
            conversation,
            priority,
            narrationID
        );
    }
}