using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class BoardView : MonoBehaviour, IBoardView
{

}

public interface IBoardView { }

public class BoardViewFactory : IBoardViewFactory
{
    public IBoardView View { get; private set; }

    readonly int rows;
    readonly int columns;
    readonly Transform transform;

    public BoardViewFactory(ISlotViewFactory slotFactory, int rows, int columns, Transform parent)
    {
        this.rows = rows;
        this.columns = columns;
        CreateSelf(parent, out transform);
        CreateSlots(slotFactory);
    }

    private void CreateSelf(Transform parent, out Transform transform)
    {
        GameObject prefab = Resources.Load<GameObject>("View/Board");
        transform = UnityEngine.Object.Instantiate(prefab).transform;
        transform.name = prefab.name;
        transform.transform.SetParent(parent);
        View = transform.GetComponent<IBoardView>();
    }

    private void CreateSlots(ISlotViewFactory slotFactory)
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slotFactory.Create(transform);
            }
        }
    }
}

public interface IBoardViewFactory
{
    IBoardView View { get; }
}