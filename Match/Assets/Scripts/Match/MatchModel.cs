using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchModel : IMatchModel
{
    public event EventHandler<SwapEventArgs> Swapping;

    public IBoardModel board { get; }
    private ISlotModel firstSelectedSlot;
    private ISlotModel secondSelectedSlot;

    public MatchModel(IBoardModel board)
    {
        this.board = board;
    }

    public void SelectedSlot(ISlotModel newSelectedTile)
    {
        if (firstSelectedSlot == null)
            SelectFirstTile(newSelectedTile);
        else if (IsSelectedTheSameTile(newSelectedTile))
            DeselectTile(ref firstSelectedSlot);
        else
            SelectSecondTile(newSelectedTile);
    }

    private void SelectFirstTile(ISlotModel newSelectedTile)
    {
        firstSelectedSlot = newSelectedTile;
    }

    private void DeselectBothTiles()
    {
        DeselectTile(ref firstSelectedSlot);
        DeselectTile(ref secondSelectedSlot);
    }

    private void DeselectTile(ref ISlotModel selectedTile)
    {
        selectedTile.IsSelected = false;
        selectedTile = null;
    }

    private void SelectSecondTile(ISlotModel newSelectedTile)
    {
        secondSelectedSlot = newSelectedTile;

        OnSwap();
        DeselectBothTiles();
    }

    private bool IsSelectedTheSameTile(ISlotModel newSelectedTile)
    {
        return newSelectedTile.Position == firstSelectedSlot.Position;
    }

    private void OnSwap()
    {
        GameObject content1 = firstSelectedSlot.Content;
        firstSelectedSlot.Content = secondSelectedSlot.Content;
        secondSelectedSlot.Content = content1;

        Swapping?.Invoke(this, new SwapEventArgs(firstSelectedSlot, secondSelectedSlot));
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