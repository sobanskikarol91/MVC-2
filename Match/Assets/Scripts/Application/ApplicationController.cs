using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] ApplicationModel model;

    private void Awake()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        SlotViewFactory slotFactory = new SlotViewFactory();

        BoardViewFactory factory = new BoardViewFactory(slotFactory, 5, 5, transform);
        IBoardView view = factory.View;
    }
}