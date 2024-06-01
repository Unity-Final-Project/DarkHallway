using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public float animationDelay = 3f; 

    public void StartGame()
    {
        StartCoroutine(TransitionToLoadingScreen());
    }

    public void QuitGame()
    {
        sceneLoader.QuitGame();
    }
    private IEnumerator TransitionToLoadingScreen()
    {
        yield return new WaitForSeconds(animationDelay);
        sceneLoader.LoadSceneByIndex(1);
    }
}
