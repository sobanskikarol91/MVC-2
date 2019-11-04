using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class MatchSearcher : MonoBehaviour
{
    private ISlotModel previousSlot, currentSlot;
    private int sequenceLength = 3;
    private List<ISlotModel> matchingSlotsToErase = new List<ISlotModel>();
    private ICompareGameObjects comparison;


    public List<ISlotModel> GetMatchSequences(IBoardModel board, int sequenceLength, ICompareGameObjects comparison)
    {
        this.comparison = comparison;
        this.sequenceLength = sequenceLength;
        SearchRows(board);
        SearchColumns(board);
        matchingSlotsToErase = matchingSlotsToErase.Distinct().ToList();
        return matchingSlotsToErase;
    }

    private void SearchRows(IBoardModel board)
    {
        List<ISlotModel> matches = new List<ISlotModel>();

        // nie sprawdzac ostatnich
        for (int r = 0; r < board.Rows; r++)
        {
            for (int c = 0; c < board.Columns; c++)
            {
                currentSlot = board.Slots[r, c];
                CheckTileInSequence(matches);
                previousSlot = currentSlot;
               // Debug.Log(board.Slots[r, c].Position + "Matching Amount:" + matches.Count);
            }

            CheckIfSequenceIsLongEnough(matches);
            matches.Clear();
        }
    }

    private void SearchColumns(IBoardModel board)
    {
        List<ISlotModel> matches = new List<ISlotModel>();

        // nie sprawdzac ostatnich
        for (int c = 0; c < board.Columns; c++)
        {
            for (int r = 0; r < board.Rows; r++)
            {
                currentSlot = board.Slots[r, c];
                CheckTileInSequence(matches);
                previousSlot = currentSlot;
               // Debug.Log(board.Slots[r, c].Position + "Matching Amount:" + matches.Count);
            }

            CheckIfSequenceIsLongEnough(matches);
            matches.Clear();
        }
    }

    private void CheckTileInSequence(List<ISlotModel> matches)
    {
        if (matches.Count == 0 || IsTheSameAsPreviousSlot())
            matches.Add(currentSlot);
        else
            FoundDifferentSlot(matches);
    }

    private bool IsSequenceFound()
    {
        return matchingSlotsToErase.Count == sequenceLength;
    }

    private void FoundDifferentSlot(List<ISlotModel> matches)
    {
        CheckIfSequenceIsLongEnough(matches);

        matches.Clear();
        matches.Add(currentSlot);
    }

    private void CheckIfSequenceIsLongEnough(List<ISlotModel> matches)
    {
        if (matches.Count >= sequenceLength)
            matchingSlotsToErase.AddRange(matches);
    }

    private bool IsTheSameAsPreviousSlot()
    {
        // po losowaniu mozna wywalic nulla
        if (currentSlot.Content == null || previousSlot.Content == null) return false;

        List<GameObject> toCompare = new List<GameObject> { currentSlot.Content, previousSlot.Content };
        return comparison.AreEqual(toCompare);
    }
}

public interface ICompareGameObjects
{
    bool AreEqual(List<GameObject> gameObjects);
}

public class MatchColorComparation : ICompareGameObjects
{
    public bool AreEqual(List<GameObject> gameObjects)
    {
        if (gameObjects.Count <= 1) return false;

        Color color = gameObjects.First().GetComponent<Image>().color;
        return gameObjects.TrueForAll(g => g.GetComponent<Image>().color == color);
    }
}