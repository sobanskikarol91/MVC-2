using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface ITileController { }

public class TileController : ControllerMVC<ITileModel, ITileView>, ITileController
{
    public TileController(ITileModel model, ITileView view) : base(model, view) {  }
}