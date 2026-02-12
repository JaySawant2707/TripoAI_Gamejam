using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTimer : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] int sceneIndex;
    [SerializeField] GameObject video;
    private void Start()
    {
        StartCoroutine(OpenNextScene());
    }

    IEnumerator OpenNextScene()
    {
        if (video != null)
        {
            yield return new WaitForSeconds(12f);
            video.SetActive(false);
        }
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
