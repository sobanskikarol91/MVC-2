using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchModel : IMatchModel
{
    public IBoardModel board { get; }
}

public interface IMatchModel
{
    IBoardModel board { get; }
}