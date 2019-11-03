using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchView : IMatchView
{
    public IBoardView Board { get; }

    public MatchView(IBoardView board)
    {
        Board = board;
    }
}

public interface IMatchView
{
    IBoardView Board { get; }
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