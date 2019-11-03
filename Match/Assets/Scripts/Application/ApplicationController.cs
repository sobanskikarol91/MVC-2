using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] ApplicationModel settings;

    private int rows;
    private int columns;


    private void Awake()
    {
        rows = settings.Rows;
        columns = settings.Columns;
        GameObject[,] slotContent = GetRandomGameObjects();
        CreateBoard(slotContent);
    }

    GameObject[,] GetRandomGameObjects()
    {
        Color[,] colors = new Color[rows, columns];
        SeedGenerator.SetRandomNotRepeatingCollection(ref colors, settings.TileColors, settings.ColorsAmount, settings.Seed);

        GameObject TilePrefab = settings.TilePrefab;
        GameObject[,] tiles = new GameObject[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                tiles[r, c] = Instantiate(settings.TilePrefab);
                TilePrefab.GetComponent<Image>().color = colors[r, c];
            }
        }

        return tiles;
    }

    void CreateBoard(GameObject[,] slotContent)
    {
        BoardModelFactory modelFactory = new BoardModelFactory();
        IBoardModel model = modelFactory.Create(new SlotModelFactory(), rows, columns);

        BoardViewFactory viewFactory = new BoardViewFactory();
        IBoardView view = viewFactory.Create(new SlotViewFactory(), rows, columns, transform);

        BoardControllerFactory controllerFactory = new BoardControllerFactory();
        IBoardController controller = controllerFactory.Create(new SlotControllerFactory(), model, view);

        model.SetSlotsContent(slotContent);
    }
}