using UnityEngine.UI;  
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for Coroutines (delays)

public class MainMenuController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource buttonSound; // Drag your AudioSource here in Inspector
    
    [Header("Settings")]
    public float loadDelay = 0.5f; // Delay in seconds before loading scene
    public string gameSceneName = "Test"; // Name of your game scene

    // Function for the Play Button
    public void PlayGame()
    {
        // 1️⃣ Play the button sound
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // 2️⃣ Start the delayed scene load
        StartCoroutine(LoadSceneAfterDelay());
    }

    // Coroutine: waits, then loads the scene
    private IEnumerator LoadSceneAfterDelay()
    {
        // Optional: Disable the button so player can't click twice
        Button playButton = GetComponentInChildren<Button>();
        if (playButton != null) playButton.interactable = false;

        // Wait for the delay time
        yield return new WaitForSeconds(loadDelay);

        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    // Function for the Exit Button
    public void QuitGame()
    {
        Debug.Log("Game Exited!");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); 
        #endif
    }
}