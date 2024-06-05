using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private bool isLoadingComplete = false;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.OnLoadingComplete += HandleLoadingComplete;
    }

    void Update()
    {
        if (isLoadingComplete && Input.anyKeyDown)
        {
            sceneLoader.LoadSceneByIndex(2);
        }
    }

    private void HandleLoadingComplete()
    {
        isLoadingComplete = true;
    }
}
