using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCrop : Tool
{
    public CropGrowth _cropGrowth;

    public GameObject loot;
    public int yieldAmount = 3;
    public float spread = 0.0935f;

    private void Start() {
        _cropGrowth = gameObject.GetComponentInParent<CropGrowth>();
    }

    public override void Hit() {
        Vector3 pos = transform.position;

        float spreadOffset = UnityEngine.Random.value - spread / 2;
        while (yieldAmount > 0) {
            yieldAmount -= 1;

            GameObject GO = Instantiate(loot);
            GO.transform.localPosition = new Vector2(transform.position.x + spreadOffset, transform.position.y + spreadOffset);
            
        }
       
    }
}
