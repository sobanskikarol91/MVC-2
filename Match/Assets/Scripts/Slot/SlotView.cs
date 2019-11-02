using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SlotView : MonoBehaviour, ISlotView
{

}

public interface ISlotView { }

public class SlotViewFactory : ISlotViewFactory
{
    public ISlotView Create(Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("View/Slot");
        GameObject instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = prefab.name;
        instance.transform.SetParent(parent, false);
        return instance.GetComponent<ISlotView>();
    }
}

public interface ISlotViewFactory
{
    ISlotView Create(Transform parent);
}