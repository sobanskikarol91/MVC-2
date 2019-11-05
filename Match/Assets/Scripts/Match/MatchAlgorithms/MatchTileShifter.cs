using UnityEngine;


public class MatchTileShifter
{
    private readonly int rows;
    private readonly int columns;
    private readonly ISlotModel[,] slots;
    private int emptySlotsInColumn;

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
            emptySlotsInColumn = 0;

            for (int r = rows - 1; r > emptySlotsInColumn; r--)
            {
                if (slots[r, c].Content == null)
                {
                    ShiftTilesDown(r, c);
                    emptySlotsInColumn++;
                    r++;
                }
            }

            SetEmptySlotsOnColumnTop(c);
        }
    }

    private void ShiftTilesDown(int rStart, int c)
    {
        for (int r = rStart; r >= emptySlotsInColumn + 1; r--)
        {
            GameObject content = slots[r, c].Content;
            slots[r, c].Content = slots[r - 1, c].Content;
        }
    }

    void SetEmptySlotsOnColumnTop(int c)
    {
        for (int r = 0; r < emptySlotsInColumn; r++)
        {
            slots[r, c].Content = null;
        }
    }
}