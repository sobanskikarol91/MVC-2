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

    private void CreateSlots()
    {
        SlotViewFactory factory = new SlotViewFactory(transform);
        ISlotView slot = factory.View;
    }

    void CreateBoard()
    {
        BoardViewFactory factory = new BoardViewFactory(transform);
        IBoardView view = factory.View;
    }
}