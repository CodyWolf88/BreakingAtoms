using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip introClip;
    public AudioClip loopClip;

    [Header("Audio Sources")]
    public AudioSource introSource;
    public AudioSource loopSource;

    [Header("Timing (Seconds)")]
    [Tooltip("Positive = Delay after intro. Negative = Overlap to fix gaps.")]
    [Range(-0.5f, 0.5f)] 
    public float transitionOffset = 0.5f; 

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

        if (introSource == null) introSource = gameObject.AddComponent<AudioSource>();
        if (loopSource == null) loopSource = gameObject.AddComponent<AudioSource>();

        introSource.playOnAwake = false;
        loopSource.playOnAwake = false;
        loopSource.loop = true;

        PlayMusicSequence();
    }

    public void PlayMusicSequence()
    {
        if (introClip == null || loopClip == null) return;

        double startTime = AudioSettings.dspTime;

        // 1. Play Intro
        introSource.clip = introClip;
        introSource.PlayScheduled(startTime);

        // 2. Schedule Loop
        // Formula: Start Time + Intro Length + Your Offset
        double loopStartTime = startTime + introClip.length + transitionOffset;
        
        loopSource.clip = loopClip;
        loopSource.PlayScheduled(loopStartTime);
        
        Invoke(nameof(StopIntroSource), (float)introClip.length + 0.1f);
    }

    void StopIntroSource()
    {
        introSource.Stop();
    }

    public void RestartMusic()
    {
        loopSource.Stop();
        introSource.Stop();
        PlayMusicSequence();
    }
}