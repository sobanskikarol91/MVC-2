using UnityEngine;
using UnityEngine.UI;


public class Application : MonoBehaviour
{
    [SerializeField] ApplicationSettings settings;

    private int rows;
    private int columns;

    private IBoardModel boardModel;
    private IBoardView boardView;
    private IBoardController boardController;


    private void Awake()
    {
        rows = settings.Rows;
        columns = settings.Columns;
        GameObject[,] slotContent = GetRandomTiles();
        CreateBoard(slotContent);
        CreateMatch();
    }

    private void CreateMatch()
    {
        GameObject[] slotContentPrefabs = new GameObject[settings.ColorsAmount];

        for (int i = 0; i < slotContentPrefabs.Length; i++)
        {
            GameObject instance = slotContentPrefabs[i] = Instantiate(settings.TilePrefab);

            int nr = Random.Range(0, settings.ColorsAmount);
            instance.GetComponent<Image>().color = settings.TileColors[nr];
        }

        // I can use here factory pattern, but I haven't time for it...
        IMatchModel model = new MatchModel(boardModel, settings.MatchSequenceLength, slotContentPrefabs);
        IMatchView view = new GameObject("MatchView").AddComponent<MatchView>();
        view.Init(boardView);

        IMatchController controller = new MatchController(model, view);
    }

    private GameObject[,] GetRandomTiles()
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