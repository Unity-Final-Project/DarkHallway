using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public float animationDelay = 3f; // Adjust this to match your animation duration

    public void StartGame()
    {
        StartCoroutine(TransitionToLoadingScreen());
    }

    private IEnumerator TransitionToLoadingScreen()
    {
        // Wait for the animation to complete
        yield return new WaitForSeconds(animationDelay);

        // Load the Loading Screen scene (assuming it's at index 1)
        sceneLoader.LoadSceneByIndex(1);
    }
}
