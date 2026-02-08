using System.Collections;
using TMPro;
using UnityEngine;

public class PlaceIllusion : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] GameObject tooth;

    void Start()
    {
        tooth.SetActive(false);
        portal.SetActive(false);
    }

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
        tooth.SetActive(true);
        portal.SetActive(true);
        interactionText.text = "Tooth Placed!";
        yield return new WaitForSeconds(2f);
        interactionText.gameObject.SetActive(false);
    }
}