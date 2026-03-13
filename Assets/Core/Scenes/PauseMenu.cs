using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    [Header("UI")]
    public GameObject pauseMenuUI; // Drag PauseCanvas here
    
    [Header("Scenes")]
    public string mainMenuScene = "MainMenu"; // Name of your main menu scene

    void Update()
    {
        // Check if ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
                //Debug.Log("s-a apasat esc");
            }
            else
            {
                PauseGame();
                //Debug.Log("s-a apasat esc");
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the menu
        Time.timeScale = 1f; // Unpause time
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show the menu
        Time.timeScale = 0f; // Freeze time
        GameIsPaused = true;
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f; // Unpause before leaving
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Unpause before quitting
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}