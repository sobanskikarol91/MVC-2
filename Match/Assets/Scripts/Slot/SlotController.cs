using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SlotController : ControllerMVC<ISlotModel, ISlotView>, ISlotController
{
    public SlotController(ISlotModel model, ISlotView view) : base(model, view) { }
}

public interface ISlotController { }

public interface ISlotControllerFactory
{
    ISlotController Create(ISlotModel model, ISlotView view);
}

public class SlotControllerFactory : ISlotControllerFactory
{
    public ISlotController Create(ISlotModel model, ISlotView view)
    {
        return new SlotController(model, view);
    }
}