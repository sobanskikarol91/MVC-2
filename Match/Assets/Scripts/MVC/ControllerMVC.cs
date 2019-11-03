public class ControllerMVC<M, V>
{
    protected readonly M model;
    protected readonly V view;

    public ControllerMVC(M model, V view)
    {
        this.model = model;
        this.view = view;
    }
}