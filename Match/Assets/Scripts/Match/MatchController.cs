using System;


public class MatchController : ControllerMVC<IMatchModel, IMatchView>, IMatchController
{
    public IBoardController[] Board { get; }


    public MatchController(IMatchModel model, IMatchView view) : base(model, view)
    {
        SubscribingSelectedSlots(model);
    }

    private void SubscribingSelectedSlots(IMatchModel model)
    {
        ISlotModel[,] slots = model.board.Slots;

        for (int r = 0; r < model.board.Rows; r++)
        {
            for (int c = 0; c < model.board.Columns; c++)
            {
                slots[r, c].Selected += HandleSelectedSlot;
            }
        }
    }

    private void HandleSelectedSlot(object sender, EventArgs e)
    {
        ISlotModel slot = sender as ISlotModel;
        model.SelectedSlot(slot);
    }
}

public interface IMatchController
{
    IBoardController[] Board { get; }
}