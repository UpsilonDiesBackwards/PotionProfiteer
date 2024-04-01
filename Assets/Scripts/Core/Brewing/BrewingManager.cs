using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BrewingManager : MonoBehaviour {
    public List<PotionRecipe> potionRecipes = new List<PotionRecipe>();

    public List<InventoryItemData> cauldronContents = new List<InventoryItemData>();

    public InventoryItemData tomato;
    public InventoryItemData chilli;

    public void Update() {
        if (Input.GetKeyUp(KeyCode.I)) {
            Debug.Log("Added Tomato");
            AddIngredient(tomato);
        }

        if (Input.GetKeyUp(KeyCode.O)) {
            Debug.Log("Added Chilli");
            AddIngredient(chilli);
        }

        if (Input.GetKeyUp(KeyCode.P)) {
            Debug.Log("Confirming ingredients...");
            ConfirmIngredients();
        }
    }

    public void AddIngredient(InventoryItemData ingredientItem) {
        cauldronContents.Add(ingredientItem);
    }

    public void ConfirmIngredients() {
        bool matchFound = false;

        foreach (PotionRecipe recipe in potionRecipes) {
            if (AreListsEqual(cauldronContents, recipe.ingredients)) {
                Debug.Log("Recipe match found: " + recipe.name);
                matchFound = true;
                break;
            }

            if (!matchFound) {
                Debug.Log("No recipe found for those ingredients!");
            }
        }

        cauldronContents.Clear();
    }

    bool AreListsEqual(List<InventoryItemData> cauldron, InventoryItemData[] recipe) // probably uneccesary for this to be a seperate method but for readability sake 
    {
        if (cauldron.Count != recipe.Length) // compare length of list first
            return false;

        for (int i = 0; i < cauldron.Count; i++) // then check contents
        {
            if (cauldron[i] != recipe[i])
                return false;
        }

        return true;
    }
}
