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

        BoardModelFactory modelFactory = new BoardModelFactory(new SlotModelFactory(), rows, columns);
        IBoardModel model = modelFactory.Model;

        BoardViewFactory viewFactory = new BoardViewFactory(new SlotViewFactory(), rows, columns, transform);
        IBoardView view = viewFactory.View;

        BoardControllerFactory controllerBoardFactory = new BoardControllerFactory();
        IBoardController controller = controllerBoardFactory.Create(new SlotControllerFactory(), model, view);
    }
}