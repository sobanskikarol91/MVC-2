using System;
using UnityEngine;
using UnityEngine.UI;


public class MatchView : IMatchView
{
    public event Action HighlightedMatchesEnd;
    public event Action ErasedMatchesEnd;
    public IBoardView Board { get; }


    public MatchView(IBoardView board)
    {
        Board = board;
    }

    public void HighlightMatches(Vector2[] matches)
    {
        Action<int, int> action = (row, columns) => Board.Slots[row, columns].Content.GetComponent<Image>().color = Color.black;
        DoActionForMatches(matches, action);
        HighlightedMatchesEnd?.Invoke();
    }

    public void EraseMatches(GameObject[] matches)
    {
        for (int i = 0; i < matches.Length; i++)
            matches[i].SetActive(false);


        // TODO: Fade animation (Corutine)
        ErasedMatchesEnd?.Invoke();
    }

    void DoActionForMatches(Vector2[] matches, Action<int, int> DoAction)
    {
        for (int i = 0; i < matches.Length; i++)
        {
            int row = (int)matches[i].x;
            int columns = (int)matches[i].y;

            DoAction(row, columns);
        }
    }
}

public interface IMatchView
{
    event Action HighlightedMatchesEnd;
    event Action ErasedMatchesEnd;
    IBoardView Board { get; }
    void HighlightMatches(Vector2[] matches);
    void EraseMatches(GameObject[] matches);
}

public interface IMatchViewFactory
{
    IMatchView Create(IBoardView board);
}

public class MatchViewFactory : IMatchViewFactory
{
    public IMatchView Create(IBoardView board)
    {
        return new MatchView(board);
    }
}