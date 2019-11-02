using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlotModel : ScriptableObject, ISlotModel
{

}

public interface ISlotModel { }

public class SlotModelFactory : ISlotModelFactory
{
    public ISlotModel Create()
    {
        SlotModel original = Resources.Load<SlotModel>("Model/Slot");
        return ScriptableObject.Instantiate(original);
    }
}

public interface ISlotModelFactory
{
    ISlotModel Create();
}