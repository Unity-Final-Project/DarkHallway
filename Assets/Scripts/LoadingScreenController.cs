using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    private SceneLoader sceneLoader;

    void Start()
    {
        // Find the SceneLoader in the scene
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Load Level 1 by its index (assuming it's at index 2)
            sceneLoader.LoadSceneByIndex(2);
        }
    }
}
