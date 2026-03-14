using System.Linq;
using UnityEngine;

public class BookClick : MonoBehaviour
{
    public GameObject bookUI; // Drag your UI Panel here

    void OnMouseDown()
    {
        bookUI.SetActive(true);
        Debug.Log("Book was clicked!"); // This will show up in the 'Console' tab
    }

}

