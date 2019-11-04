using UnityEngine;


public class MatchTileShifter
{
    private readonly int rows;
    private readonly int columns;
    private readonly ISlotModel[,] slots;


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
            slots[r, c].Content = slots[r - 1, c].Content;
        }

        slots[0, c].Content = null;

        if (rStart != 0 && slots[rStart, c].Content == null)
            ShiftTilesDown(rStart, c);
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