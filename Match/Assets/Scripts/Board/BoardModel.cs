﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardModel : IBoardModel
{
    public ISlotModel[,] Slots { get; }
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

public interface IBoardModelFactory
{
    IBoardModel Model { get; }
}

public class BoardModelFactory : IBoardModelFactory
{
    public IBoardModel Model { get; }

    public BoardModelFactory(ISlotModelFactory slotFactory, int rows, int columns)
    {
        ISlotModel[,] slots = new ISlotModel[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c] = slotFactory.Create();
            }
        }

        Model = new BoardModel(slots);
    }
}