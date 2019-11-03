public class BoardController : ControllerMVC<IBoardModel, IBoardView>, IBoardController
{
    ISlotController[,] Slots;

    public BoardController(ISlotController[,] slots, IBoardModel model, IBoardView view) : base(model, view)
    {
        Slots = slots;
    }
}

public interface IBoardController { }

public interface IBoardControllerFactory
{
    IBoardController Create(ISlotControllerFactory slotFactory, IBoardModel model, IBoardView view);
}

public class BoardControllerFactory : IBoardControllerFactory
{
    public IBoardController Create(ISlotControllerFactory slotFactory, IBoardModel model, IBoardView view)
    {
        int rows = model.Rows;
        int columns = model.Columns;

        ISlotController[,] slots = new ISlotController[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c] = slotFactory.Create(model.Slots[r, c], view.Slots[r, c]);
            }
        }

        return new BoardController(slots, model, view);
    }
}