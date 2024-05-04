using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace PPPS.Core{

public class SkinManager : MonoBehaviour
{
    public GameObject playerSkin;

    public SpriteRenderer spriteRend;
    public List<Sprite> skins = new List<Sprite>();

    private int _selectedSkin = 0;

    public string sceneToLoad;

    public void NextOption() { // Advance upwards through skins list and display current skin.
        _selectedSkin = _selectedSkin + 1;

        if (_selectedSkin == skins.Count) {
            _selectedSkin = 0;
        }
        spriteRend.sprite = skins[_selectedSkin];
    }

    public void BackOption() {  // Advance downwards through skins list and display current skin.
        _selectedSkin = _selectedSkin - 1;
        
        if (_selectedSkin < 0) {
            _selectedSkin = skins.Count - 1;
        }
        spriteRend.sprite = skins[_selectedSkin];
    }

    public void PlayGame() { // Load the requested scene.
        PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/Prefabs/SelectedSkin.prefab");
        SceneManager.LoadScene(sceneToLoad);
        // GameManager.Instance.playerSkin = playerSkin;
    }
}
}