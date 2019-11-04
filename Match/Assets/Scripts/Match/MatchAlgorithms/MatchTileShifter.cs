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
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if (slots[r, c].Content == null)
                {
                    Debug.Log("Empty found");
                    ShiftTilesDown(r, c);
                }
                else
                    Debug.Log("Not null: " + r + " " + c, slots[r, c].Content);
            }
        }
    }

    private void ShiftTilesDown(int r, int cStart)
    {
        List<ISlotModel> emptySlots = new List<ISlotModel>();
        int shiftStep = 0;

        for (int y = cStart; y < columns; y++)
        {
            GameObject content = slots[r, y].Content;

            if (content == null)
                shiftStep++;

            emptySlots.Add(slots[r, y]);
        }

        for (int i = 0; i < shiftStep; i++)
        {
            for (int k = 0; k < emptySlots.Count - 1; k++)
            {
                Debug.Log("Shift");
                emptySlots[k].Content = emptySlots[k + 1].Content;
                emptySlots[k + 1].Content = null;

                shiftResultArgs.AddShift(emptySlots[k].Position, emptySlots[k + 1].Position);
            }
        }

        for (int i = 0; i < shiftResultArgs.Result.Count; i++)
        {
            Debug.Log(shiftResultArgs.Result[i].Origin + shiftResultArgs.Result[i].Destination);
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