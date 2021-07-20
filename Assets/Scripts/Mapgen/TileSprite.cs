using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileSprite
{    
    public Sprite TileImage;

    public TileSprite()
    {
        TileImage = null;
    }

    public TileSprite(Sprite inSprite)
    {
        TileImage = inSprite;
    }
}
