using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchModel : IMatchModel
{
    public event EventHandler<SwapEventArgs> Swap;
    public event EventHandler<FoundMatchesEventArgs> FoundMatchesSuccessful;
    public event EventHandler<EraseContentEventArgs> ErasingMatches;

    public IBoardModel Board { get; }
    public int SequenceLength { get; }
    public GameObject[] SlotContentVariants { get; }

    private List<ISlotModel> selectedSlots = new List<ISlotModel>();
    private MatchSearcher matchSearcher = new MatchSearcher();     // TODO: Create interfaces for it.
    private MatchTileShifter tileShifter;                          // TODO: Create interfaces for it.
    private List<ISlotModel> foundedMatches = new List<ISlotModel>();



    public MatchModel(IBoardModel board, int sequenceLength, GameObject[] slotContentVariants)
    {
        SlotContentVariants = slotContentVariants;
        SequenceLength = sequenceLength;
        Board = board;
        tileShifter = new MatchTileShifter(Board.Slots);
    }

    public void SelectedSlot(ISlotModel newSelected)
    {
        if (IsntAnySlotSelected())
            SelectFirstTile(newSelected);
        else if (IsSelectedTheSameTile(newSelected))
            DeselectSlots();
        else
            SelectSecondTile(newSelected);
    }

    private bool IsntAnySlotSelected()
    {
        return selectedSlots.Count == 0;
    }

    private void SelectFirstTile(ISlotModel newSelectedSlot)
    {
        selectedSlots.Add(newSelectedSlot);
    }

    private void SelectSecondTile(ISlotModel newSelectedSlot)
    {
        selectedSlots.Add(newSelectedSlot);
        OnSwap();
        DeselectSlots();
    }

    private void DeselectSlots()
    {
        selectedSlots.ForEach(s => s.Deselect());
        selectedSlots.Clear();
    }

    private bool IsSelectedTheSameTile(ISlotModel newSelectedTile)
    {
        return selectedSlots?.First() == newSelectedTile;
    }

    private void OnSwap()
    {
        GameObject firstContent = selectedSlots.First().Content;

        selectedSlots.First().Content = selectedSlots.Last().Content;
        selectedSlots.Last().Content = firstContent;

        Swap?.Invoke(this, new SwapEventArgs(selectedSlots.First(), selectedSlots.Last()));
    }

    public void FindMatch()
    {
        foundedMatches = matchSearcher.GetMatchSequences(Board, SequenceLength, new MatchColorComparation());

        if (foundedMatches.Count > 0)
            OnMatchesFound();
    }

    private void OnMatchesFound()
    {
        FoundMatchesSuccessful?.Invoke(this, new FoundMatchesEventArgs(foundedMatches));
    }

    // Create new class for this content
    public void OnErasingMatches()
    {
        GameObject[] contentToErase = foundedMatches.Select(f => f.Content).ToArray();
        foundedMatches.ForEach(c => c.Content = null);
        ErasingMatches?.Invoke(this, new EraseContentEventArgs(contentToErase));
    }

    public void ShiftTiles()
    {
        tileShifter.ShiftDownTiles();
        FillEmptyTiles();
        //FindMatch();
    }

    ISlotModel[] GetEmptySlots()
    {
        List<ISlotModel> emptySlots = new List<ISlotModel>();
        for (int r = 0; r < Board.Rows; r++)
        {
            for (int c = 0; c < Board.Columns; c++)
            {
                if (Board.Slots[r, c].Content == null)
                    emptySlots.Add(Board.Slots[r, c]);
                else
                    Debug.Log(Board.Slots[r, c].Position, Board.Slots[r, c].Content);
            }
        }

        return emptySlots.ToArray();
    }

    void FillEmptyTiles()
    {
        ISlotModel[] emptySlots = GetEmptySlots();
        Debug.Log("Fill empty spaces");

        for (int i = 0; i < emptySlots.Length; i++)
        {
            int nr = UnityEngine.Random.Range(0, SlotContentVariants.Length);
            GameObject randomGO = SlotContentVariants[nr];

            Debug.Log("Filling: " + emptySlots[i].Position, emptySlots[i].Content);
            emptySlots[i].Content = GameObject.Instantiate(randomGO);

        }
    }
}

public class EraseContentEventArgs : EventArgs
{
    public GameObject[] ToErase { get; }

    public EraseContentEventArgs(GameObject[] toErase)
    {
        ToErase = toErase;
    }
}

public class FoundMatchesEventArgs : EventArgs
{
    public Vector2[] Positions { get; }

    public FoundMatchesEventArgs(List<ISlotModel> matches)
    {
        Positions = matches.Select(m => m.Position).ToArray();
    }
}

public class SwapEventArgs : EventArgs
{
    public SwapEventArgs(ISlotModel slot1, ISlotModel slot2)
    {
        Slot1 = slot1;
        Slot2 = slot2;
    }

    public ISlotModel Slot1 { get; private set; }
    public ISlotModel Slot2 { get; private set; }
}

public interface IMatchModel
{
    event EventHandler<SwapEventArgs> Swap;
    event EventHandler<FoundMatchesEventArgs> FoundMatchesSuccessful;
    event EventHandler<EraseContentEventArgs> ErasingMatches;

    int SequenceLength { get; }
    IBoardModel Board { get; }
    GameObject[] SlotContentVariants { get; }

    void SelectedSlot(ISlotModel newSelectedTile);
    void OnErasingMatches();
    void FindMatch();
    void ShiftTiles();
}