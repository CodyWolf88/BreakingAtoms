using System;
using UnityEngine;

public enum StateOfMatter
{
    Solid,
    Liquid,
    Gas
}

public class Script_Element : MonoBehaviour
{
    
    public string elementName = "";
    public StateOfMatter state =  StateOfMatter.Solid;
    
    private Rigidbody2D rigidBody;
    private Vector2 offset;
    private bool isDragging = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (state == StateOfMatter.Gas)
        {
            rigidBody.gravityScale = -0.25f;
        }
        else
        {
            rigidBody.gravityScale = 1f;
        }
    }

    void OnMouseDown()
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        rigidBody.linearVelocity = Vector2.zero;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = new Vector2(transform.position.x, transform.position.y) - new Vector2(mousePos.x, mousePos.y);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }

    }

    private void OnMouseUp()
    {
        isDragging = false;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
