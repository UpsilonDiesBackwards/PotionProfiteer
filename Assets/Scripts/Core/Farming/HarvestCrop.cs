using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCrop : Tool
{
    public CropGrowth _cropGrowth;

    public GameObject loot;
    public static int yieldAmount = 2;
    public float spawnRadius = 1f;

    private void Start() {
        _cropGrowth = gameObject.GetComponentInParent<CropGrowth>();
    }

    public override void Hit() {
        for (int i = 0; i < yieldAmount; i++) {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Instantiate(loot, spawnPos, Quaternion.identity);
        }
    }
}
