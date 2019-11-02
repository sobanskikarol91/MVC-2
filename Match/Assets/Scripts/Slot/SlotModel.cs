using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SlotModel : ISlotModel
{
    public Vector2 Position { get; }
    public ITileModel Tile { get; }


    public SlotModel(Vector2 position, ITileModel tile = null)
    {
        Position = position;
        Tile = tile;
    }
}

public interface ISlotModel
{
    Vector2 Position { get; }
}

public class SlotModelFactory : ISlotModelFactory
{
    public ISlotModel Create(Vector2 position)
    {
        return new SlotModel(position);
    }
}

public interface ISlotModelFactory
{
    ISlotModel Create(Vector2 position);
}