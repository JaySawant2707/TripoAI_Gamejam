using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI orgasm;
    private bool isPickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(ShowMessageRoutine());
        }
    }

    IEnumerator ShowMessageRoutine()
    {
        isPickedUp = true;
        orgasm.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        orgasm.gameObject.SetActive(false);
    }
}
