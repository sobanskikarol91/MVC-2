using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GridLayoutGroup))]
public class BoardView : MonoBehaviour, IBoardView
{
    GridLayoutGroup gridLayout;

    private ISlotView[,] slots;


    public ISlotView[,] Slots
    {
        get => slots;
        set { slots = value; UpdateGridLayout(); }
    }

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
    }

    private void UpdateGridLayout()
    {
        if (!gridLayout) gridLayout = GetComponent<GridLayoutGroup>();

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayout.constraintCount = slots.GetLength(0);
    }

    public void DisableInteraction()
    {
        for (int r = 0; r < slots.GetLength(0); r++)
        {
            for (int c = 0; c < slots.GetLength(1); c++)
            {
                slots[r, c].DisableInteraction();
            }
        }
    }

    public void EnableInteraction()
    {
        for (int r = 0; r < slots.GetLength(0); r++)
        {
            for (int c = 0; c < slots.GetLength(1); c++)
            {
                slots[r, c].EnableInteraction();
            }
        }
    }
}

public interface IBoardView
{
    ISlotView[,] Slots { get; set; }

    void EnableInteraction();
    void DisableInteraction();
}

public class BoardViewFactory : IBoardViewFactory
{
    private int rows;
    private int columns;
    private Transform transform;


    public IBoardView Create(ISlotViewFactory slotFactory, int rows, int columns, Transform parent)
    {
        this.rows = rows;
        this.columns = columns;
        transform = parent;
        IBoardView view = CreateSelf(parent);
        view.Slots = GetAndCreateSlots(slotFactory);
        return view;
    }

    IBoardView CreateSelf(Transform parent)
    {
        IBoardView view;
        GameObject prefab = Resources.Load<GameObject>("View/Board");
        transform = UnityEngine.Object.Instantiate(prefab).transform;
        transform.name = prefab.name;
        transform.transform.SetParent(parent, false);
        view = transform.GetComponent<IBoardView>();
        return view;
    }

    private ISlotView[,] GetAndCreateSlots(ISlotViewFactory slotFactory)
    {
        ISlotView[,] slots = new ISlotView[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c] = slotFactory.Create(transform);
            }
        }

        return slots;
    }
}

public interface IBoardViewFactory
{
    IBoardView Create(ISlotViewFactory slotFactory, int rows, int columns, Transform parent);
}