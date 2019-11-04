using UnityEngine;


public class MatchTileShifter
{
    private readonly int rows;
    private readonly int columns;
    private readonly ISlotModel[,] slots;
    private int emptySlotsInColumn = 0;

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
           CountEmptySlots(c);

            for (int r = rows - 1; r >= 0; r--)
            {
                if (slots[r, c].Content == null)
                {
                    ShiftTilesDown(r, c);
                    SetEmptySlotsOnColumnTop(c);
                    break;
                }
            }
        }
    }

    void CountEmptySlots(int c)
    {
       emptySlotsInColumn = 0;

        for (int r = 0; r < rows; r++)
        {
            if (slots[r, c].Content == null)
                emptySlotsInColumn++;
        }
    }

    private void ShiftTilesDown(int rStart, int c)
    {
        Debug.Log(rStart + " " + emptySlotsInColumn);

        for (int r = rStart; r >= emptySlotsInColumn; r--)
        {
            GameObject content = slots[r, c].Content;
            Debug.Log(slots[r, c].Position + " New: " + (r - emptySlotsInColumn) );
            slots[r, c].Content = slots[r - emptySlotsInColumn, c].Content;
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