using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;        // ← Required for Button class
using System.Collections;

public class MainMenuController : MonoBehaviour  // ← Class name must match file name!
{
    [Header("Audio")]
    public AudioSource buttonSound;
    
    [Header("Settings")]
    public float loadDelay = 1f;
    public string gameSceneName = "Test";

    // ✅ Must be 'public' to appear in Button OnClick
    public void PlayGame()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        Button playButton = GetComponentInChildren<Button>();
        if (playButton != null) 
        {
            playButton.interactable = false;
        }

        yield return new WaitForSeconds(loadDelay);

        SceneManager.LoadScene(gameSceneName);
    }

    // ✅ Must be 'public' to appear in Button OnClick
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