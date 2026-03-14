using UnityEngine;
using UnityEngine.UI;

public class BookSystem : MonoBehaviour
{
    public Sprite[] pages; // Drag your 6 page sprites here
    public Image pageDisplay; // The UI Image component showing the page
    public AudioClip[] pageTurnSounds; // Drag your 6 sounds here

    private int currentPage = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdatePage();
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
            PlayRandomSound();
        }
    }

    void UpdatePage()
    {
        pageDisplay.sprite = pages[currentPage];
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pageTurnSounds.Length);
        audioSource.PlayOneShot(pageTurnSounds[randomIndex]);
    }

    public void OpenBook() => gameObject.SetActive(true);
    public void CloseBook() => gameObject.SetActive(false);
}