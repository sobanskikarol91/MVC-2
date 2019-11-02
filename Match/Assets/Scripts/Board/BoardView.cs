using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardView : MonoBehaviour, IBoardView
{
    
}

public interface IBoardView { }

public class BoardViewFactory : IBoardViewFactory
{
    public IBoardView View { get; private set; }

    public BoardViewFactory(Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>("View/Board");
        GameObject instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = prefab.name;
        instance.transform.SetParent(parent);
        View = instance.GetComponent<IBoardView>();
    }
}

public interface IBoardViewFactory
{
    IBoardView View { get; }
}