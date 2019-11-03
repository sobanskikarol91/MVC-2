using System;
using System.Linq;
using UnityEngine;

public class MatchController : ControllerMVC<IMatchModel, IMatchView>, IMatchController
{
    public IBoardController[] Board { get; }


    public MatchController(IMatchModel model, IMatchView view) : base(model, view)
    {
        SubscribingSlotsEvents(model);
        model.Swap += HandleSwap;
        model.FoundMatchesSuccessful += HandleFoundMatches;
        model.ErasingMatches += HandleEraseMatches;
    }

    private void HandleEraseMatches(object sender, MatchesEventArgs e)
    {
        Vector2[] positions = e.Matches.Select(m => m.Position).ToArray();
        view.EraseMatches(positions);
    }

    private void HandleFoundMatches(object sender, MatchesEventArgs e)
    {
        Vector2[] positions = e.Matches.Select(m => m.Position).ToArray();
        view.HighlightMatches(positions);
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