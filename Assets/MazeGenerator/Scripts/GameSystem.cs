using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameSystem1 : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        int collectedPoints = FirstPersonController.coinPoint;
        if (collectedPoints == MazeSpawner.TotalPoints)
        {
            WinGame();
        }
        bool isDead = FirstPersonController.isAlive;
        if (!isDead)
        {
            LoseGame();
        }

    }
    void WinGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Player win");
    }
    void LoseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        losePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Player lose");
    }
}
