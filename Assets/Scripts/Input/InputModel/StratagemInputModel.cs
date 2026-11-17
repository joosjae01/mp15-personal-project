using UnityEngine;

public class StratagemInputModel
{
    public ObservableProperty<Vector2> CommandInput;

    public StratagemInputModel()
    {
        CommandInput = new(Vector2.zero);
    }
}
