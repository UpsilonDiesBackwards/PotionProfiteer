using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BrewingManager : MonoBehaviour {
    public List<PotionRecipe> potionRecipes = new List<PotionRecipe>();

    public List<ItemData> cauldronContents = new List<ItemData>();

    private AudioSource source;
    public AudioClip plopClip;
    public AudioClip errorClip;
    public AudioClip createdPotionClip;

    public GameObject emptyPotion;

    public GameObject resource;
    public GameObject potionSpawn;
    public float range = 2f;
    public LayerMask resourceLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource")) {

            ItemData itemData = other.gameObject.GetComponent<Item>().itemData;
            AddIngredient(itemData);
            Destroy(other.gameObject);
        }
    }

    public void AddIngredient(ItemData ingredientItem) {
        cauldronContents.Add(ingredientItem);

        if (ingredientItem != null)
        {
            PlayAudio(plopClip);
            Debug.Log("Item added to cauldron: " + ingredientItem.itemName);
        }
    }

    public void ConfirmIngredients() {
        bool matchFound = false;

        foreach (PotionRecipe recipe in potionRecipes) {
            if (AreListsEqual(cauldronContents, recipe.ingredients)) {
                Debug.Log("Recipe match found: " + recipe.name);
                PlayAudio(createdPotionClip);
                
                GetComponentInChildren<ItemDrop>().item = recipe.potion;
                GetComponentInChildren<ItemDrop>().DropItem();
                
                matchFound = true;
                break;
            }
           
        }
        if (!matchFound)
        {
            Debug.Log("No recipe found for those ingredients!");
            Instantiate(emptyPotion, potionSpawn.transform.position, Quaternion.identity);
            PlayAudio(errorClip);
        }
        cauldronContents.Clear();
    }

    bool AreListsEqual(List<ItemData> cauldron, ItemData[] recipe) {
        if (cauldron.Count != recipe.Length) // Compare length of list first
            return false;

        for (int i = 0; i < cauldron.Count; i++) { // Then check contents
            if (cauldron[i] != recipe[i])
                return false;
        }

        return true;
    }

    void PlayAudio(AudioClip clip) {
        if (!source) {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.PlayOneShot(clip, 0.7f);
    }
}
