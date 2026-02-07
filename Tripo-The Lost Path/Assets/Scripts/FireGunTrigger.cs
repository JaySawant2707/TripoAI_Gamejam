using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class FireGunTrigger : MonoBehaviour
{
    [Header("Fire Timing")]
    [Tooltip("How long the fire stays active")]
    [SerializeField] private float fireOnDuration = 1.5f;

    [Tooltip("How long the fire stays off before firing again")]
    [SerializeField] private float fireOffDuration = 2f;

    [Header("Fire Collider")]
    [SerializeField] private Collider fireTriggerCollider;

    [Header("Particles")]
    [SerializeField] ParticleSystem particleSys;

    [Header("Lights")]
    [SerializeField] Light fireLight;
    [SerializeField] private float fireLightIntensity = 8f;
    [SerializeField] private float lightFadeDuration = 0.15f;

    private Coroutine fireRoutine;
    private Tween lightTween;

    AudioSource au;

    private void Awake()
    {
        if (fireTriggerCollider != null)
            fireTriggerCollider.enabled = false;

        au = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        fireRoutine = StartCoroutine(FireLoop());
    }

    private void OnDisable()
    {
        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        if (fireTriggerCollider != null)
            fireTriggerCollider.enabled = false;
    }

    private System.Collections.IEnumerator FireLoop()
    {
        while (true)
        {
            // 🔥 Fire ON
            au.Play();
            particleSys.Play();
            yield return new WaitForSeconds(0.3f);
            if (fireLight != null)
            {
                lightTween?.Kill();
                lightTween = fireLight
                    .DOIntensity(fireLightIntensity, lightFadeDuration)
                    .SetEase(Ease.OutQuad);
            }
            fireTriggerCollider.enabled = true;
            yield return new WaitForSeconds(fireOnDuration);

            // ❄ Fire OFF
            au.Stop();
            if (fireLight != null)
            {
                lightTween?.Kill();
                lightTween = fireLight
                    .DOIntensity(0f, lightFadeDuration)
                    .SetEase(Ease.InQuad);
            }
            fireTriggerCollider.enabled = false;
            particleSys.Stop();
            yield return new WaitForSeconds(fireOffDuration);
        }
    }
}
