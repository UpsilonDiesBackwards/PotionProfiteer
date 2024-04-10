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

    public GameObject resource;
    public float range = 2f;
    public LayerMask resourceLayer;

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

        DetectCollision();
    }

    void DetectCollision()
    {
        Collider2D[] brewing = Physics2D.OverlapCircleAll(resource.transform.position, range, resourceLayer);

        foreach(Collider2D c in brewing)
        {
            Debug.Log("Its in the Pot");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On Trigger");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On STAYING in the Pot");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On Collision");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On STAY Collision");
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
