using System;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] ApplicationModel settings;

    private int rows;
    private int columns;

    IBoardModel boardModel;
    IBoardView boardView;
    IBoardController boardController;


    private void Awake()
    {
        rows = settings.Rows;
        columns = settings.Columns;
        GameObject[,] slotContent = GetRandomGameObjects();
        CreateBoard(slotContent);
        CreateMatch();
    }

    private void CreateMatch()
    {
        IMatchModel model = new MatchModel(boardModel, settings.MatchSequenceLength);
        IMatchView view = new MatchView(boardView);
        IMatchController controller = new MatchController(model, view);
    }

    GameObject[,] GetRandomGameObjects()
    {
        Color[,] colors = new Color[rows, columns];
        SeedGenerator.SetRandomNotRepeatingCollection(ref colors, settings.TileColors, settings.Seed);

        GameObject TilePrefab = settings.TilePrefab;
        GameObject[,] tiles = new GameObject[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                tiles[r, c] = Instantiate(settings.TilePrefab);
                tiles[r, c].GetComponent<Image>().color = colors[r, c];
            }
        }

        return tiles;
    }

    void CreateBoard(GameObject[,] slotContent)
    {
        BoardModelFactory modelFactory = new BoardModelFactory();
        boardModel = modelFactory.Create(new SlotModelFactory(), rows, columns);

        BoardViewFactory viewFactory = new BoardViewFactory();
        boardView = viewFactory.Create(new SlotViewFactory(), rows, columns, transform);

        BoardControllerFactory controllerFactory = new BoardControllerFactory();
        boardController = controllerFactory.Create(new SlotControllerFactory(), boardModel, boardView);

        boardModel.SetSlotsContent(slotContent);
    }
}