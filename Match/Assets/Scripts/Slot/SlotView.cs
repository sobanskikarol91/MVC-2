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

    public SlotViewFactory()
    {
        View = new SlotView();
    }
}

public interface ISlotViewFactory
{
    ISlotView View { get; }
}