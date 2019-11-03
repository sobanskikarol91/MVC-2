using System;
using UnityEngine;


public class SlotModel : ISlotModel
{
    public event EventHandler Selected;
    public event EventHandler Deselected;
    public event EventHandler<ContentChangedEventArgs> ContentChanged;
    public Vector2 Position { get; }

    private GameObject content;
    public GameObject Content
    {
        get => content;
        set
        {

            if (content == value) return;
            content = value;
            Debug.Log("Set model content");
            ContentChanged?.Invoke(this, new ContentChangedEventArgs(content));
        }
    }

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

public class ContentChangedEventArgs : EventArgs
{
    public GameObject Content { get; }

    public ContentChangedEventArgs(GameObject content)
    {
        Content = content;
    }
}

public interface ISlotModel
{
    event EventHandler Selected;
    event EventHandler Deselected;
    event EventHandler<ContentChangedEventArgs> ContentChanged;
    GameObject Content { get; set; }
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