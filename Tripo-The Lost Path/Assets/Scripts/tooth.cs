using System.Collections;
using TMPro;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI orgasm;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(ShowMessageRoutine());
        }
    }

    IEnumerator ShowMessageRoutine()
    {
        orgasm.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        orgasm.gameObject.SetActive(false);
    }
}
