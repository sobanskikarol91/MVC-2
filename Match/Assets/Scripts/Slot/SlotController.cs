using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface ISlotController { }

public class SlotController : ControllerMVC<ISlotView, ISlotModel>, ISlotController
{
    public SlotController(ISlotView model, ISlotModel view) : base(model, view) { }
}