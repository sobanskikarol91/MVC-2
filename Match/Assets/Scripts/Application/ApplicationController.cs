using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] ApplicationModel settings;

    private void Awake()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        int rows = settings.Rows;
        int columns = settings.Columns;
        
        BoardModelFactory modelBoardFactory = new BoardModelFactory(new SlotModelFactory(), rows, columns);
        IBoardModel boardModel = modelBoardFactory.Model;

        BoardViewFactory viewBoardfactory = new BoardViewFactory(new SlotViewFactory(), rows, columns, transform);
        IBoardView boardView = viewBoardfactory.View;
    }
}