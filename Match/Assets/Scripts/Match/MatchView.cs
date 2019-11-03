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
        for (int i = 0; i < matches.Length; i++)
        {
            int row = (int)matches[i].x;
            int columns = (int)matches[i].y;
            Board.Slots[row, columns].Content.GetComponent<Image>().color = Color.black;
        }
    }
}

public interface IMatchView
{
    IBoardView Board { get; }
    void HighlightMatches(Vector2[] matches);
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