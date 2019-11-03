using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileModel : ITileModel 
{
    public Color color { get; set; }

    public TileModel(Color color)
    {
        this.color = color;
    }
}

public interface ITileModel { }