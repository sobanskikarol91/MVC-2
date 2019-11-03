using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardModel : IBoardModel
{
    public ISlotModel[,] Slots { get; }
    public int Rows { get => Slots.GetLength(0); }
    public int Columns { get => Slots.GetLength(1); }

    public void SetSlotsContent(GameObject[,] content)
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                Slots[r,c].Content = content[r,c];
            }
        }
    }

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
    void SetSlotsContent(GameObject[,] content);
}

public interface IBoardModelFactory
{
    IBoardModel Create(ISlotModelFactory slotFactory, int rows, int columns);
}

public class BoardModelFactory : IBoardModelFactory
{
    public IBoardModel Create(ISlotModelFactory slotFactory, int rows, int columns)
    {
        ISlotModel[,] slots = new ISlotModel[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c] = slotFactory.Create(new Vector2(r, c));
            }
        }

        return new BoardModel(slots);
    }
}