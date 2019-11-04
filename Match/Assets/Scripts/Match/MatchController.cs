using System;


public class MatchController : ControllerMVC<IMatchModel, IMatchView>, IMatchController
{
    public IBoardController[] Board { get; }


    public MatchController(IMatchModel model, IMatchView view) : base(model, view)
    {
        SubscribingSlotsEvents(model);
        model.Swap += HandleSwap;
        model.FoundMatchesSuccessful += HandleFoundMatches;
        model.ErasingMatches += HandleEraseMatches;

        view.HighlightedMatchesEnd += model.OnErasingMatches;
        //view.ErasedMatchesEnd += model.ShiftTiles;
    }

    private void HandleEraseMatches(object sender, MatchesEventArgs e)
    {
        view.EraseMatches(e.Positions);
    }

    private void HandleFoundMatches(object sender, MatchesEventArgs e)
    {
        view.HighlightMatches(e.Positions);
    }

    private void HandleSwap(object sender, SwapEventArgs e)
    {
        model.FindMatch();
    }

    private void SubscribingSlotsEvents(IMatchModel model)
    {
        ISlotModel[,] slots = model.Board.Slots;

        for (int r = 0; r < model.Board.Rows; r++)
        {
            for (int c = 0; c < model.Board.Columns; c++)
            {
                slots[r, c].Clicked += HandleClickedSlot;
            }
        }
    }

    private void HandleClickedSlot(object sender, EventArgs e)
    {
        ISlotModel slot = sender as ISlotModel;
        model.SelectedSlot(slot);
    }
}

public interface IMatchController
{
    IBoardController[] Board { get; }
}