using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public enum ReactionCondition
{
    Normal,
    Heat,
    Cold,
    Electricity
}

public class Script_ReactionFlask : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class Recipe
    {
        public string recipeName;
        public List<string> requiredSymbols;

        public ReactionCondition reactionCondition = ReactionCondition.Normal;
    }

    [SerializeField]
    private TextMeshProUGUI elementsText;
    
    [Header("Recipes")]
    public List<Recipe> allRecipes;
    
    [Header("Elements in flask")]
    public List<string> elementsInFlask;
    
    public ReactionCondition currentCondition = ReactionCondition.Normal;
    private void CheckReactionFlask()
    {
        elementsInFlask.Sort();
        string currentSignature = string.Join(",", elementsInFlask);
        
        string elementsIn_Text = string.Join("+", elementsInFlask);
        elementsText.text = elementsIn_Text;

        foreach (Recipe recipe in allRecipes)
        {
            recipe.requiredSymbols.Sort();
            string recipeSignature = string.Join(",", recipe.requiredSymbols);

            if (currentSignature == recipeSignature && currentCondition == recipe.reactionCondition)
            {
                //elementsInFlask.Clear();
                
                Debug.Log(recipe.recipeName);
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
