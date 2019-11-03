using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchModel : IMatchModel
{
    public event EventHandler<SwapEventArgs> Swap;
    public event EventHandler<MatchFoundEventArgs> FoundMatchesSuccessful;
    public IBoardModel Board { get; }
    public int SequenceLength { get; }

    private List<ISlotModel> selectedSlots = new List<ISlotModel>();
    private MatchSearcher matchSearcher = new MatchSearcher();     // TODO: Create interfaces for it.
    private MatchEraser matchEraser;


    public MatchModel(IBoardModel board, int sequenceLength)
    {
        SequenceLength = sequenceLength;
        Board = board;
    }

    public void SelectedSlot(ISlotModel newSelected)
    {
        Debug.Log("choose state");

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
        Debug.Log("Selected");
        selectedSlots.Add(newSelectedSlot);
    }

    private void SelectSecondTile(ISlotModel newSelectedSlot)
    {
        Debug.Log("Swap");
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
        List<ISlotModel> matches = matchSearcher.GetMatchSequences(Board, SequenceLength, new MatchColorComparation());

        Debug.Log("Matches:" + matches.Count);

        if (matches.Count > 0)
            OnMatchesFound(matches);
    }

    void OnMatchesFound(List<ISlotModel> matches)
    {
        FoundMatchesSuccessful?.Invoke(this, new MatchFoundEventArgs(matches));
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

public class MatchFoundEventArgs : EventArgs
{
    public List<ISlotModel> Matches { get; } = new List<ISlotModel>();

    public MatchFoundEventArgs(List<ISlotModel> matches)
    {
        Matches = matches;
    }
}

public interface IMatchModel
{
    event EventHandler<SwapEventArgs> Swap;
    event EventHandler<MatchFoundEventArgs> FoundMatchesSuccessful;

    int SequenceLength { get; }
    IBoardModel Board { get; }
    void SelectedSlot(ISlotModel newSelectedTile);
    void FindMatch();
}