using UnityEngine;
using UnityEngine.EventSystems; // Required for mouse interactions

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    public float hoverScale = 1.1f; // How much bigger it gets (1.1 = 10% bigger)

    private Vector3 originalScale;

    void Start()
    {
        // Remember the original size when the game starts
        originalScale = transform.localScale;
    }

    // Called when mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }

    // Called when mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}