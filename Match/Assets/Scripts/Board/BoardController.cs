using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IBoardController { }

public class BoardController : ControllerMVC<IBoardModel, IBoardView>, IBoardController
{
    public BoardController(IBoardModel model, IBoardView view) : base(model, view) { }
}