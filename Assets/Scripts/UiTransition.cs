using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UiTransition : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera menuCamera; 

    public void Start()
    {
        currentCamera.Priority++;
        Debug.Log("Current Camera Priority: " + currentCamera.Priority);
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;
        Debug.Log("Updated Camera Priority: " + currentCamera.Priority);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentCamera.Priority--;
            currentCamera = menuCamera;
            currentCamera.Priority++;
            Debug.Log("Switched to Menu Camera with Priority: " + currentCamera.Priority);
        }
    }
}
