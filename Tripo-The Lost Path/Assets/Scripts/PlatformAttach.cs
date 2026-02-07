using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    bool isAttatched = false;
    GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isAttatched)
            playerObject.transform.parent = this.transform.parent;
        else
            playerObject.transform.parent = null;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isAttatched = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttatched = false;
        }
    }
}
