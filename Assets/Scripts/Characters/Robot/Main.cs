using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Main : MonoBehaviour
{
    public List<Sprite> inSprites;
    public List<Sprite> ineSprites;
    public List<Sprite> ieSprites;
    public List<Sprite> iseSprites;
    public List<Sprite> isSprites;
    public List<Sprite> iswSprites;
    public List<Sprite> iwSprites;
    public List<Sprite> inwSprites;

    public List<Sprite> wnSprites;
    public List<Sprite> wneSprites;
    public List<Sprite> weSprites;
    public List<Sprite> wseSprites;
    public List<Sprite> wsSprites;
    public List<Sprite> wswSprites;
    public List<Sprite> wwSprites;
    public List<Sprite> wnwSprites;

    public List<Sprite> rnSprites;
    public List<Sprite> rneSprites;
    public List<Sprite> reSprites;
    public List<Sprite> rseSprites;
    public List<Sprite> rsSprites;
    public List<Sprite> rswSprites;
    public List<Sprite> rwSprites;
    public List<Sprite> rnwSprites;

    public List<Sprite> anSprites;
    public List<Sprite> aneSprites;
    public List<Sprite> aeSprites;
    public List<Sprite> aseSprites;
    public List<Sprite> asSprites;
    public List<Sprite> aswSprites;
    public List<Sprite> awSprites;
    public List<Sprite> anwSprites;

    public List<Sprite> snSprites;
    public List<Sprite> sneSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sseSprites;
    public List<Sprite> ssSprites;
    public List<Sprite> sswSprites;
    public List<Sprite> swSprites;
    public List<Sprite> snwSprites;

    public List<Sprite> hnSprites;
    public List<Sprite> hneSprites;
    public List<Sprite> heSprites;
    public List<Sprite> hseSprites;
    public List<Sprite> hsSprites;
    public List<Sprite> hswSprites;
    public List<Sprite> hwSprites;
    public List<Sprite> hnwSprites;

    public Rigidbody2D rb;
    public Vector2 direction;
    public Transform rotationTracker;
    public float cursorAngle;
    public float directionDegrees;

    public float moveSpeed = 4f;
    public float sprintModifierF;
    public float sprintModifierFD;
    public bool sprintBool = false;

    public int spriteDirection;

    float idleTime;
    public float framerate;

    public SpriteRenderer spriteRenderer;

    void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        directionDegrees = ((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 270) % 360;

        rb.velocity = direction * moveSpeed * GetSprintSpeed();

        cursorAngle = rotationTracker.eulerAngles.z;

        Animate();
    }

    void Animate()
    {
        List<Sprite> currentSprites = null;

        spriteDirection = Mathf.FloorToInt(((cursorAngle + 382.5f) % 360) / 45);

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            currentSprites = GetIdleSprite();
        }
        else if (sprintBool == true)
        {
            currentSprites = GetRunSprite();
        }
        else if (rb.velocity.x > 0 && rb.velocity.y > 0)
        {
            currentSprites = GetWalkSprite();
        }

        float playTime = Time.time - idleTime;
        int totalFrames = (int)(playTime * framerate);
        int frame = totalFrames % currentSprites.Count;

        spriteRenderer.sprite = currentSprites [frame];
    }

    float GetSprintSpeed()
    {
        float sprint;

        float diff = Mathf.Abs(directionDegrees - cursorAngle);

        diff = Mathf.Min(diff, Mathf.Abs(diff - 360));

        if (diff < 22.5)
        {
            sprint = sprintModifierF;
            sprintBool = true;
        }
        else if (diff < 67.5)
        {
            sprint = sprintModifierFD;
        }
        else
        {
            sprint = 1;
        }

        return sprint;
    }

    List<Sprite> GetIdleSprite()
    {
        List<Sprite>[] idleSprites = {
            inSprites,
            inwSprites,
            iwSprites,
            iswSprites,
            isSprites,
            iseSprites,
            ieSprites,
            ineSprites
        };

        List<Sprite> selectedSprites = idleSprites [spriteDirection];

        return selectedSprites;
    }

    List<Sprite> GetWalkSprite()
    {
        List<Sprite>[] walkSprites = {
            wnSprites,
            wnwSprites,
            wwSprites,
            wswSprites,
            wsSprites,
            wseSprites,
            weSprites,
            wneSprites
        };

        List<Sprite> selectedSprites = walkSprites[spriteDirection];

        return selectedSprites;
    }

    List<Sprite> GetRunSprite()
    {
        List<Sprite>[] runSprites = {
            rnSprites,
            rnwSprites,
            rwSprites,
            rswSprites,
            rsSprites,
            rseSprites,
            reSprites,
            rneSprites
        };

        List<Sprite> selectedSprites = runSprites[spriteDirection];

        return selectedSprites;
    }
}
