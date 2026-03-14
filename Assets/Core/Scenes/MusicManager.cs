using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip introClip;
    public AudioClip loopClip;

    [Header("Audio Sources (Assign 2 in Inspector)")]
    public AudioSource introSource;
    public AudioSource loopSource;

    void Awake()
    {
        // Singleton & DontDestroyOnLoad Logic
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup Sources if not assigned manually
        if (introSource == null) introSource = gameObject.AddComponent<AudioSource>();
        if (loopSource == null) loopSource = gameObject.AddComponent<AudioSource>();

        // Configure Sources
        introSource.playOnAwake = false;
        loopSource.playOnAwake = false;
        loopSource.loop = true; // The second clip loops

        // Start the sequence
        PlayMusicSequence();
    }

    public void PlayMusicSequence()
    {
        if (introClip == null || loopClip == null) return;

        // 1. Get the current precise audio time
        double startTime = AudioSettings.dspTime;

        // 2. Setup Intro
        introSource.clip = introClip;
        introSource.volume = 1f;
        // Play the intro immediately
        introSource.PlayScheduled(startTime);

        // 3. Setup Loop
        loopSource.clip = loopClip;
        loopSource.volume = 1f;
        // Schedule the loop to start EXACTLY when the intro ends
        double loopStartTime = startTime + introClip.length;
        loopSource.PlayScheduled(loopStartTime);
        
        // Optional: Stop intro source completely after it finishes to save resources
        // (Though PlayScheduled only plays once, this ensures it's reset)
        Invoke(nameof(StopIntroSource), (float)introClip.length + 0.1f);
    }

    void StopIntroSource()
    {
        introSource.Stop();
    }

    // Call this if you need to restart music (e.g. returning to menu)
    public void RestartMusic()
    {
        loopSource.Stop();
        introSource.Stop();
        PlayMusicSequence();
    }
}