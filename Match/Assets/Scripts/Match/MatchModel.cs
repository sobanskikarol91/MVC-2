using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchModel : IMatchModel
{
    public event EventHandler<FoundMatchesEventArgs> FoundMatchesSuccessful;
    public event EventHandler<EraseContentEventArgs> ErasingMatches;
    public event Action Shifting;
    public event Action FillingEmptySlots;
    public event Action FoundMatchFailed;

    public IBoardModel Board { get; }
    public int SequenceLength { get; }
    public GameObject[] SlotContentVariants { get; }
    public MatchInteraction Interaction { get; }

    private MatchSearcher matchSearcher = new MatchSearcher();     // TODO: Create interfaces for it.
    private MatchTileShifter tileShifter;                          // TODO: Create interfaces for it.
    private List<ISlotModel> foundedMatches = new List<ISlotModel>();


    public MatchModel(IBoardModel board, int sequenceLength, GameObject[] slotContentVariants)
    {
        SlotContentVariants = slotContentVariants;
        SequenceLength = sequenceLength;
        Board = board;

        tileShifter = new MatchTileShifter(Board.Slots);
        Interaction = new MatchInteraction();
    }

    public void FindMatch()
    {
        foundedMatches = matchSearcher.GetMatchSequences(Board, SequenceLength, new MatchColorComparation());

        if (foundedMatches.Count > 0)
            OnMatchesFound();
        else
            OnNoMatchesFound();
    }

    private void OnNoMatchesFound()
    {
        FoundMatchFailed?.Invoke();
    }

    private void OnMatchesFound()
    {
        FoundMatchesSuccessful?.Invoke(this, new FoundMatchesEventArgs(foundedMatches));
    }

    public void OnErasingMatches()
    {
        GameObject[] contentToErase = foundedMatches.Select(f => f.Content).ToArray();
        foundedMatches.ForEach(c => c.Content = null);
        ErasingMatches?.Invoke(this, new EraseContentEventArgs(contentToErase));
    }

    public void OnShiftTiles()
    {
        tileShifter.ShiftDownTiles();
        Shifting?.Invoke();
    }

    public void OnFillEmptySlots()
    {
        ISlotModel[] emptySlots = GetEmptySlots();

        for (int i = 0; i < emptySlots.Length; i++)
        {
            int nr = UnityEngine.Random.Range(0, SlotContentVariants.Length);
            GameObject randomGO = SlotContentVariants[nr];

            emptySlots[i].Content = GameObject.Instantiate(randomGO);
        }

        FillingEmptySlots?.Invoke();
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
            }
        }

        return emptySlots.ToArray();
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
    event EventHandler<FoundMatchesEventArgs> FoundMatchesSuccessful;
    event EventHandler<EraseContentEventArgs> ErasingMatches;
    event Action FoundMatchFailed;
    event Action Shifting;
    event Action FillingEmptySlots;

    MatchInteraction Interaction { get; }
    int SequenceLength { get; }
    IBoardModel Board { get; }
    GameObject[] SlotContentVariants { get; }

    void OnFillEmptySlots();
    void OnErasingMatches();
    void FindMatch();
    void OnShiftTiles();
}