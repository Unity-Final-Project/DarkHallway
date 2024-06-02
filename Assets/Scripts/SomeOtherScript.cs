using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    private SceneLoader sceneLoader;

    void Start()
    {
        // Find the SceneLoader in the scene
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            sceneLoader.LoadSceneByIndex(1);
        }
    }
}
