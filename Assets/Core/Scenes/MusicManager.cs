using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip introClip;      // Plays once
    public AudioClip loopClip;       // Plays on loop after intro

    [Header("Settings")]
    public bool playIntroOnStart = true;

    private AudioSource audioSource;
    private bool isIntroPlaying = false;

    void Awake()
    {
        // Singleton pattern with duplicate check
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false; // We'll control playback manually

        // Start playback
        if (playIntroOnStart && introClip != null)
        {
            PlayIntro();
        }
        else if (loopClip != null)
        {
            PlayLoop();
        }
    }

    public void PlayIntro()
    {
        if (introClip == null)
        {
            Debug.LogWarning("Intro clip not assigned!");
            return;
        }

        isIntroPlaying = true;
        audioSource.clip = introClip;
        audioSource.loop = false;
        audioSource.Play();

        // Start coroutine to switch to loop clip after intro ends
        StartCoroutine(SwitchToLoopAfterIntro());
    }

    private System.Collections.IEnumerator SwitchToLoopAfterIntro()
    {
        // Wait for intro clip to finish
        yield return new WaitForSeconds(introClip.length);

        // Switch to loop clip
        PlayLoop();
    }

    public void PlayLoop()
    {
        if (loopClip == null)
        {
            Debug.LogWarning("Loop clip not assigned!");
            return;
        }

        isIntroPlaying = false;
        audioSource.clip = loopClip;
        audioSource.loop = true;

        // Only play if not already playing the loop clip
        if (audioSource.clip != loopClip || !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Optional: Call this if you want to restart intro (e.g., returning to main menu)
    public void RestartMusic()
    {
        StopAllCoroutines();
        PlayIntro();
    }
}