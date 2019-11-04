using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchInteraction
{
    private List<ISlotModel> selectedSlots = new List<ISlotModel>();
    public event EventHandler<SwapEventArgs> Swap;


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

        if (AreSlotsNeighbors())
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

    private bool AreSlotsNeighbors()
    {
        Vector2 difference = selectedSlots.First().Position - selectedSlots.Last().Position;
        Vector2 distance = new Vector2(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
        return distance.x == 1 && distance.y == 0 || distance.x == 0 && distance.y == 1;
    }
}