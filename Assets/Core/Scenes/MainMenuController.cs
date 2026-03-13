using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class MainMenuController : MonoBehaviour
{
    // Function for the Play Button
    public void PlayGame()
    {
        // Load the scene named "Game"
        SceneManager.LoadScene("Game");
    }

    // Function for the Exit Button
    public void QuitGame()
    {
        Debug.Log("Game Exited!"); // Shows message in console when testing
        
        #if UNITY_EDITOR
            // Stops play mode if testing inside Unity Editor
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            // Actually quits the game if built as an .exe or app
            Application.Quit(); 
        #endif
    }
}