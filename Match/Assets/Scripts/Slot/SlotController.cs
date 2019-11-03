using System;


public class SlotController : ControllerMVC<ISlotModel, ISlotView>, ISlotController
{
    public SlotController(ISlotModel model, ISlotView view) : base(model, view)
    {
        view.Clicked += HandleSlotClicked;
        model.ContentChanged += HandleContentChanged;
        model.Selected += HandleSelectedSlot;
        model.Deselected += HandleDeselectedSlot;
    }

    private void HandleContentChanged(object sender, ContentChangedEventArgs e)
    {
        view.Content = e.Content;
    }

    private void HandleSlotClicked(object sender, OnSlotClickedEventArgs e)
    {
        model.IsSelected ^= true;
    }

    private void HandleSelectedSlot(object sender, EventArgs e)
    {
        view.Select();
    }

    private void HandleDeselectedSlot(object sender, EventArgs e)
    {
        view.Deselect();
    }
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