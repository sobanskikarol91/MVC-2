using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlotModel : ISlotModel
{
    
}

public interface ISlotModel { }

public class SlotModelFactory : ISlotModelFactory
{
    public ISlotModel Model { get; private set; }

    public SlotModelFactory()
    {
        Model = new SlotModel();
    }
}

public interface ISlotModelFactory
{
    ISlotModel Model { get; }
}