using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Script_ReactionFlask : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class Recipe
    {
        public string recipeName;
        public List<string> requiredSymbols;
    }
    
    [Header("Recipes")]
    public List<Recipe> allRecipes;
    
    [Header("Elements in flask")]
    public List<string> elementsInFlask;

    private void CheckReactionFlask()
    {
        elementsInFlask.Sort();
        string currentSignature = string.Join(",", elementsInFlask);

        foreach (Recipe recipe in allRecipes)
        {
            recipe.requiredSymbols.Sort();
            string recipeSignature = string.Join(",", recipe.requiredSymbols);

            if (currentSignature == recipeSignature)
            {
                elementsInFlask.Clear();
                
                Debug.Log("Table salt");
                return;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Script_Element element = other.GetComponent<Script_Element>();

        if (element != null)
        {
            elementsInFlask.Add(element.elementSymbol);
            
            Destroy(other.gameObject);
            
            CheckReactionFlask();
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
