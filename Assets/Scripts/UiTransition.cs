using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UiTransition : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    public void Start()
    {
        currentCamera.Priority++;
        Debug.Log("...."+currentCamera.Priority);
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;
        Debug.Log(currentCamera.Priority);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentCamera.Priority--;
        }
    }
}
