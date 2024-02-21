using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropGrowth : MonoBehaviour {
    public SpriteRenderer spriteRend;
    public Sprite[] sprites;
    public static int currentSprite;

    public float timer;
    public float maxTimer = 4.0f;

    public static bool fullyGrown = false;
    private bool _canGrow = true;

    void Update() {
        if (_canGrow == true) { timer += Time.deltaTime; }

        GrowingGreen();
    }

    void GrowingGreen() {
        if (timer > maxTimer && currentSprite < sprites.Length && fullyGrown == false) {
            currentSprite++;
            spriteRend.sprite = sprites[currentSprite];
            fullyGrown = false;

            if (currentSprite == sprites.Length) {
                timer = maxTimer;
                _canGrow = false;
                fullyGrown = true; //if sprite thats loaded is the last one in the spriteArray max it out
            }
            else
            {
                _canGrow = true;
            }
            timer = 0;
        }
    }
}
