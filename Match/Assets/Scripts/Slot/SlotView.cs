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
    public ISlotView View { get; private set; }

    public SlotViewFactory(Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>("View/Slot");
        GameObject instance = UnityEngine.Object.Instantiate(prefab);
        instance.name = prefab.name;
        instance.transform.SetParent(parent);
        View = instance.GetComponent<ISlotView>();
    }
}

public interface ISlotViewFactory
{
    ISlotView View { get; }
}