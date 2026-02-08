using System.Collections;
using TMPro;
using UnityEngine;

public class PlaceIllusion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(PlacingJugad());
    }

    IEnumerator PlacingJugad()
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = "Placing Tooth...";
        yield return new WaitForSeconds(1.5f);
        interactionText.text = "Tooth Placed!";
        yield return new WaitForSeconds(2f);
        interactionText.gameObject.SetActive(false);
    }
}