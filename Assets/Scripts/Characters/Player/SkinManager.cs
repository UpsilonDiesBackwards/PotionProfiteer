using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;

public class SkinManager : MonoBehaviour
{
    public GameObject playerSkin;

    public SpriteRenderer spriteRend;
    public List<Sprite> skins = new List<Sprite>();

    private int _selectedSkin = 0;

    public void NextOption() {
        
    }
}
