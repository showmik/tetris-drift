using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transionTime = 1f;

    public void GoToScene(int buildIndex)
    {
        StartCoroutine(LoadLevel(buildIndex));
    }

    IEnumerator LoadLevel(int buildIndex)
    {
        FindObjectOfType<AudioManager>().Play("MenuSelect");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
