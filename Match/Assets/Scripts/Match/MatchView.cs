using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MatchView : MonoBehaviour, IMatchView
{
    public event Action HighlightedMatchesEnd;
    public event Action ErasedMatchesEnd;
    public IBoardView Board { get; private set; }

    private float animationTime = 0.2f;

    public void Init(IBoardView board)
    {
        Board = board;
    }

    public void HighlightMatches(Vector2[] matches)
    {
        StartCoroutine(IEHighlightedMatch(matches));
    }

    IEnumerator IEHighlightedMatch(Vector2[] matches)
    {
        Action<int, int> action = (row, columns) => Board.Slots[row, columns].Content.GetComponent<Image>().color = Color.black;
        DoActionForMatches(matches, action);

        yield return new WaitForSeconds(animationTime);
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
    void Init(IBoardView board);
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
        return new MatchView();
    }
}