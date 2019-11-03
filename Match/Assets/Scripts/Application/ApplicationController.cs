using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplicationController : MonoBehaviour
{
    [SerializeField] ApplicationModel settings;

    private int rows;
    private int columns;


    private void Awake()
    {
        rows = settings.Rows;
        columns = settings.Columns;
        CreateBoard();
        GenerateRandomTiles();
    }

    private void GenerateRandomTiles()
    {
        Color[,] colors = new Color[rows, columns];
        SeedGenerator.SetRandomNotRepeatingCollection(ref colors, settings.TileColors, settings.ColorsAmount, settings.Seed);
    }

    void CreateBoard()
    {
        BoardModelFactory modelFactory = new BoardModelFactory();
        IBoardModel model = modelFactory.Create(new SlotModelFactory(), rows, columns);

        BoardViewFactory viewFactory = new BoardViewFactory();
        IBoardView view = viewFactory.Create(new SlotViewFactory(), rows, columns, transform);

        BoardControllerFactory controllerFactory = new BoardControllerFactory();
        IBoardController controller = controllerFactory.Create(new SlotControllerFactory(), model, view);
    }
}