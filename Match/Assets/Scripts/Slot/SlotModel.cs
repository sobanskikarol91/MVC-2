using System;
using UnityEngine;


public class SlotModel : ISlotModel
{
    public event EventHandler Selected;
    public event EventHandler Deselected;
    public event EventHandler Clicked;
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
            ContentChanged?.Invoke(this, new ContentChangedEventArgs(content));
        }
    }

    public bool IsSelected { get; private set; }


    public SlotModel(Vector2 position, GameObject content = null)
    {
        Position = position;
        Content = content;
    }

    public void OnClick()
    {
        IsSelected ^= true;

        if (IsSelected)
            OnSelected();
        else
            OnDeselected();

        Clicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnDeselected()
    {
        Debug.Log("OnDeselct");
        Deselected.Invoke(this, EventArgs.Empty);
    }

    private void OnSelected()
    {
        Debug.Log("OnSelect");
        Selected.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        IsSelected = false;
        OnDeselected();
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
    event EventHandler Clicked;
    event EventHandler<ContentChangedEventArgs> ContentChanged;

    GameObject Content { get; set; }
    Vector2 Position { get; }
    bool IsSelected { get; }

    void OnClick();
    void Deselect();
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