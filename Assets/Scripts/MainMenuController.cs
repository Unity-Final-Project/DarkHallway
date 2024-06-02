using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public float animationDelay = 3f;

    public void StartGame()
    {
        StartCoroutine(TransitionToLoadingScreen());
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator TransitionToLoadingScreen()
    {
        yield return new WaitForSeconds(animationDelay);
        SceneManager.LoadScene("LoadingScene1");
    }
}
