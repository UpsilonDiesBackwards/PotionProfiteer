using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropGrowth : MonoBehaviour {
    public SpriteRenderer spriteRend;
    public Sprite[] sprites;
    public int currentSprite;

    public float timer;
    public float maxTimer = 4.0f;

    public bool fullyGrown = false;
    private bool _canGrow = true;

    void Update() {
        if (_canGrow) { timer += Time.deltaTime; }

        if (currentSprite == sprites.Length) { fullyGrown = true; } // currently only works if sprite is on num 8

        GrowingGreen();
    }

    void GrowingGreen() {
        if (timer > maxTimer && currentSprite < sprites.Length) {
            currentSprite++;
            spriteRend.sprite = sprites[currentSprite];

            if (currentSprite == sprites.Length) {
                currentSprite = sprites.Length;
                _canGrow = true;
            }
            timer = 0;
        }
    }
}
