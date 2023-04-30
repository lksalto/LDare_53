using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite house;
    public Sprite houseHighlight;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
