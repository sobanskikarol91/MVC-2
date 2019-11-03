using System;
using UnityEngine;


public class SlotModel : ISlotModel
{
    public event EventHandler Selected;
    public event EventHandler Deselected;
    public Vector2 Position { get; }
    public GameObject Content { get; }

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;

            if (isSelected)
                Selected.Invoke(this, EventArgs.Empty);
            else
                Deselected.Invoke(this, EventArgs.Empty);
        }
    }

    public SlotModel(Vector2 position, GameObject content = null)
    {
        Position = position;
        Content = content;
    }
}

public interface ISlotModel
{
    event EventHandler Selected;
    event EventHandler Deselected;
    Vector2 Position { get; }
    bool IsSelected { get; set; }
}

public class SlotModelFactory : ISlotModelFactory
{
    public ISlotModel Create(Vector2 position)
    {
        return new SlotModel(position);
    }
}

public interface ISlotModelFactory
{
    ISlotModel Create(Vector2 position);
}