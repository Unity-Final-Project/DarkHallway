using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    public void OnResumeButtonClicked()
    {
        ResumeGame();
        Time.timeScale = 1;
    }

    public void OnRestartLevelButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked!");
    }

    public void OnExitButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Exit is clicked");
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level2");
    }
}
