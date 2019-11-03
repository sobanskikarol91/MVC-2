using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchModel : IMatchModel
{
    public event EventHandler<SwapEventArgs> Swap;

    public IBoardModel board { get; }
    List<ISlotModel> selectedSlots = new List<ISlotModel>();

    public MatchModel(IBoardModel board)
    {
        this.board = board;
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
    IBoardModel board { get; }
    void SelectedSlot(ISlotModel newSelectedTile);
}