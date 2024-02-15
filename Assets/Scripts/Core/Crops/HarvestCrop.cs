using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCrop : Tool
{
    public GameObject loot;
    public int yieldAmount = 3;
    public float spread = 2f;

    public override void Hit() {
        float spreadOffset = UnityEngine.Random.value - spread / 2;
        while (yieldAmount > 0) {
            yieldAmount -= 1;

            Vector3 pos = transform.position;
            pos.x += spread * spreadOffset;
            pos.y += spread * spreadOffset;
            
            GameObject GO = Instantiate(loot);
            GO.transform.position = pos;
        }
        Destroy(gameObject);
    }
}
