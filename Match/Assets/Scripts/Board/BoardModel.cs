using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IBoardModel
{
    ISlotModel[,] Slots { get; }
}

public class BoardModel : IBoardModel
{
    public BoardModel(ISlotModel[,] slots)
    {
        Slots = slots;
    }

    public ISlotModel[,] Slots { get; private set; }

}