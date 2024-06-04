using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject blurPanel;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        blurPanel.SetActive(false);
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
        blurPanel.SetActive(true);

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; 
        pauseMenuPanel.SetActive(false);
        blurPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}
