using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

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
        
        public string formula;

        public ReactionCondition reactionCondition = ReactionCondition.Normal;
    }

    [SerializeField]
    private TextMeshProUGUI elementsText;
    
    [Header("Recipes")]
    public List<Recipe> allRecipes;
    
    [Header("Elements in flask")]
    public List<string> elementsInFlask;
    
    public ReactionCondition currentCondition = ReactionCondition.Normal;
    
    public void RemoveAllElements(bool resetText)
    {
        elementsInFlask.Clear();
        if (resetText)
        {
            elementsText.text = "";
        }
    }
    private bool CheckReactionFlask()
    {
        elementsInFlask.Sort();
        string currentSignature = string.Join(",", elementsInFlask);
        
        string elementsIn_Text = string.Join(" + ", elementsInFlask);
        elementsText.text = elementsIn_Text;

        foreach (Recipe recipe in allRecipes)
        {
            recipe.requiredSymbols.Sort();
            string recipeSignature = string.Join(",", recipe.requiredSymbols);

            if (currentSignature == recipeSignature && currentCondition == recipe.reactionCondition)
            {
                elementsText.text = recipe.formula;
                Debug.Log(recipe.recipeName);
                return true;
            }
        }
        
        return false;
    }

    public void ActivateHeatReaction()
    {
        currentCondition = ReactionCondition.Heat;
        if (CheckReactionFlask())
        {
            RemoveAllElements(false);
        }
    }
    
    public void ActivateColdReaction()
    {
        currentCondition = ReactionCondition.Cold;
        if (CheckReactionFlask())
        {
            RemoveAllElements(false);
        }
    }
    
    public void ActivateElectricityReaction()
    {
        currentCondition = ReactionCondition.Electricity;
        if (CheckReactionFlask())
        {
            RemoveAllElements(false);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Script_Element element = other.GetComponent<Script_Element>();

        currentCondition = ReactionCondition.Normal;
        if (element != null)
        {
            elementsInFlask.Add(element.elementSymbol);
            
            Destroy(other.gameObject);

            if (CheckReactionFlask())
            {
                RemoveAllElements(false);
            }
            
        }
    }


}
