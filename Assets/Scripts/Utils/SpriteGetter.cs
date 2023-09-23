using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteGetter : MonoBehaviour
{
    private Dictionary<int, Sprite> spriteSheetAtlas = new Dictionary<int, Sprite>();

    [SerializeField] 
    private Image image;

    public void LoadSpritesIntoDictionary(string resourcePathSpriteSheet)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(resourcePathSpriteSheet);
        spriteSheetAtlas.Clear();

        for (var i = 0; i < sprites.Length; i++)
        {
            var sprite = sprites[i];
            spriteSheetAtlas.Add(i, sprite);
        }
    }

    public void AssignSprite(int val)
    {
        image.overrideSprite = GetSprite(val);
    }

    public Sprite GetSprite(int val)
    {
        return spriteSheetAtlas[val];
    }
}
