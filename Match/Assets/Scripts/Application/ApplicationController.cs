using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour 
{
    private void Awake()
    {
        CreateBoard();
        CreateSlots();
    }

    private static void CreateSlots()
    {
        SlotViewFactory factory = new SlotViewFactory();

        ISlotView slot = factory.View;
    }

    void CreateBoard()
    {
        BoardViewFactory factory = new BoardViewFactory();
        IBoardView view = factory.View;
    }
}