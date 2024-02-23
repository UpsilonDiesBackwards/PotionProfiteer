using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CropGrowth : MonoBehaviour {
    private SpriteRenderer _spriteRend;
    public Sprite[] sprites;
    private int currentSprite;
    public float growTimer = 0;
    public float growTime = 4.0f;
    [SerializeField] private float maxGrowTimeDeviation; // We don't want every plant of the same time to grow at the same rate!


    public bool fullyGrown = false;

    void Start() {
        _spriteRend = GetComponent<SpriteRenderer>();

        growTime += Random.Range(growTime, maxGrowTimeDeviation);
    }

    void Update() {
        growTimer += Time.deltaTime;

        if (growTimer >= growTime && !fullyGrown) {
            if (currentSprite < sprites.Length - 1) {
                currentSprite++;
                _spriteRend.sprite = sprites[currentSprite];
            } else {
                fullyGrown = true;
            }

            growTimer = 0f;
        }
    }

    public void OnHarvested() {
        currentSprite = 0;
        _spriteRend.sprite = sprites[currentSprite];

        growTimer = 0;
        fullyGrown = false;
    }
}
