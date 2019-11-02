public class ControllerMVC<M, V>
{
    private readonly M model;
    private readonly V view;

    public ControllerMVC(M model, V view)
    {
        this.model = model;
        this.view = view;
    }
}