using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private static GameSystem instance;

    public GameData gameData;
    public bool isShowDialog = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            instance.Init();
        }
        else
            Destroy(gameObject);
    }

    public static GameSystem GetInstance()
    {
        return instance;
    }

    public void Init()
    {
        gameData = new GameData();
    }

}
