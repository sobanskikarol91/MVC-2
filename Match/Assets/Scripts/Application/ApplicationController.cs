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

        BoardModelFactory modelFactory = new BoardModelFactory();
        IBoardModel model = modelFactory.Create(new SlotModelFactory(), rows, columns);

        BoardViewFactory viewFactory = new BoardViewFactory();
        IBoardView view = viewFactory.Create(new SlotViewFactory(), rows, columns, transform);

        BoardControllerFactory controllerFactory = new BoardControllerFactory();
        IBoardController controller = controllerFactory.Create(new SlotControllerFactory(), model, view);
    }
}