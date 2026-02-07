using UnityEngine;

[System.Serializable]
public class NarratorLine
{
    public AudioClip clip;

    [TextArea]
    public string subtitle;

    public float delayAfter; // small pause before next line
}