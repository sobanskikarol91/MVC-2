using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardModel : IBoardModel
{
    public ISlotModel[,] Slots { get;}
    public int Rows { get => Slots.GetLength(0); }
    public int Columns { get => Slots.GetLength(1); }

    public BoardModel(ISlotModel[,] slots)
    {
        Slots = slots;
    }
}

public interface IBoardModel
{
    ISlotModel[,] Slots { get; }
    int Rows { get; }
    int Columns { get; }
}