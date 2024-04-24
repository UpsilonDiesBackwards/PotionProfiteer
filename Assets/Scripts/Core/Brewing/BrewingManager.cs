using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BrewingManager : MonoBehaviour {
    public List<PotionRecipe> potionRecipes = new List<PotionRecipe>();

    public List<InventoryItemData> cauldronContents = new List<InventoryItemData>();

    public AudioSource plop;
    public AudioSource error;
    public AudioSource createdPotion;

    public InventoryItemData tomato;
    public InventoryItemData chilli;

    public GameObject resource;
    public GameObject potionSpawn;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On Trigger");

            AddIngredient(tomato);
            //other.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            Debug.Log("ITs On STAYING in the Pot");
        }
    }

    public void AddIngredient(InventoryItemData ingredientItem) {
        cauldronContents.Add(ingredientItem);

        if (ingredientItem != null)
        {
            plop.Play();
            Debug.Log("ITEM ADDED TO CAULDRON");
        }
    }

    public void ConfirmIngredients() {
        bool matchFound = false;

        foreach (PotionRecipe recipe in potionRecipes) {
            if (AreListsEqual(cauldronContents, recipe.ingredients)) {
                Debug.Log("Recipe match found: " + recipe.name);
                createdPotion.Play();
                Instantiate(recipe.potion, potionSpawn.transform.position, Quaternion.identity);
                matchFound = true;
                break;
            }

            if (!matchFound) {
                Debug.Log("No recipe found for those ingredients!");
                error.Play();
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
