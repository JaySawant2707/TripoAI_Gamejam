using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class UIButtonFeedback : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Animation")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float animDuration = 0.15f;

    [Header("Audio")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private RectTransform rectTransform;
    private Vector3 defaultScale;
    private Tween scaleTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        scaleTween?.Kill();

        scaleTween = rectTransform
            .DOScale(defaultScale * hoverScale, animDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true); // ⭐ works when paused

        PlaySound(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scaleTween?.Kill();

        scaleTween = rectTransform
            .DOScale(defaultScale, animDuration)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || uiAudioSource == null) return;
        uiAudioSource.PlayOneShot(clip);
    }
}
