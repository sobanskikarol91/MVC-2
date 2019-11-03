using System;
using UnityEngine;
using UnityEngine.UI;


public class MatchView : IMatchView
{
    public IBoardView Board { get; }

    public MatchView(IBoardView board)
    {
        Board = board;
    }

    public void HighlightMatches(Vector2[] matches)
    {
        Action<int, int> action = (row, columns) => Board.Slots[row, columns].Content.GetComponent<Image>().color = Color.black;
        DoActionForMatches(matches, action);
    }

    public void EraseMatches(Vector2[] matches)
    {
        Action<int, int> action = (row, columns) => Board.Slots[row, columns].Content.SetActive(false);
        DoActionForMatches(matches, action);
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
    IBoardView Board { get; }
    void HighlightMatches(Vector2[] matches);
    void EraseMatches(Vector2[] matches);
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