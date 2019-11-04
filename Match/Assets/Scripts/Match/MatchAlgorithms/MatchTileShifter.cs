using System;
using System.Collections.Generic;
using UnityEngine;


public class MatchTileShifter
{
    private readonly int rows;
    private readonly int columns;
    private readonly ISlotModel[,] slots;
    private ShiftResultEventArgs shiftResultArgs = new ShiftResultEventArgs();

    public MatchTileShifter(ISlotModel[,] slots)
    {
        this.slots = slots;
        rows = slots.GetLength(0);
        columns = slots.GetLength(1);
    }

    public void ShiftDownTiles()
    {
        FindEmptySlots();
    }

    private void FindEmptySlots()
    {
        for (int c = 0; c < columns; c++)
        {
            for (int r = rows - 1; r >= 0; r--)
            {
                if (slots[r, c].Content == null)
                {
                    Debug.Log("Empty found: (" + r + " " + c + ")");
                    ShiftTilesDown(r, c);
                }
            }
        }
    }

    private void ShiftTilesDown(int rStart, int c)
    {
        for (int r = rStart; r >= 1; r--)
        {
            GameObject content = slots[r, c].Content;

            Debug.Log("(" + r + " " + c + ") Shift: " + slots[r, c].Position + " to: " + slots[r - 1, c].Position);

            slots[r, c].Content = slots[r - 1, c].Content;

            shiftResultArgs.AddShift(slots[r, c].Position, slots[r - 1, c].Position);
        }

        if (slots[rStart, c].Content == null)
            ShiftTilesDown(rStart, c);

        for (int i = 0; i < shiftResultArgs.Result.Count; i++)
        {
            Debug.Log(shiftResultArgs.Result[i].Origin + " " + shiftResultArgs.Result[i].Destination);
        }
    }
}

public class ShiftResultEventArgs : EventArgs
{
    public List<ShiftResult> Result { get; } = new List<ShiftResult>();

    public void AddShift(Vector2 origin, Vector2 destination)
    {
        Result.Add(new ShiftResult(origin, destination));
    }
}

public class ShiftResult
{
    public Vector2 Origin { get; }
    public Vector2 Destination { get; }

    public ShiftResult(Vector2 origin, Vector2 destination)
    {
        Origin = origin;
        Destination = destination;
    }
}

