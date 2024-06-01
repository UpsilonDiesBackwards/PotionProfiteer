using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New/Item")]
public class ItemData : ScriptableObject {
    public string itemName;
    public Sprite icon;

    public int value = 5;
}
