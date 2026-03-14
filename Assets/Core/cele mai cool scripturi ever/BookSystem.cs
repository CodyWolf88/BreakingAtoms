using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BookSystem : MonoBehaviour
{
    public Sprite[] pages; // Drag your 6 page sprites here
    public Image pageDisplay; // The UI Image component showing the page
    public AudioClip[] pageTurnSounds; // Drag your 6 sounds here

    public Button firstPageNextButton;
    public Button nextButton;
    public Button previousButton;
    
    private int currentPage = 0;
    private AudioSource audioSource;
    
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdatePage();
        
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
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
    
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
            PlayRandomSound();
        }
    }

    void UpdatePage()
    {
        pageDisplay.sprite = pages[currentPage];

        if (currentPage == 0)
        {
            firstPageNextButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
        }
        else if (currentPage == pages.Length - 1)
        {
            firstPageNextButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(true);
        }
        else
        {
            firstPageNextButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
        }
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, pageTurnSounds.Length);
        audioSource.PlayOneShot(pageTurnSounds[randomIndex]);
    }

    public void OpenBook() => gameObject.SetActive(true);
    public void CloseBook() => gameObject.SetActive(false);
    
}