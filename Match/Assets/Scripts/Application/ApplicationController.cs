using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour 
{
    private void Awake()
    {
        SlotViewFactory factory = new SlotViewFactory();
        ISlotView slot =  factory.View; 
    }
}