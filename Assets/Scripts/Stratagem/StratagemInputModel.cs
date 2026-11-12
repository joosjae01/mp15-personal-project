using UnityEngine;

public class StratagemInputModel
{
    public ObservableProperty<Vector2> CommandInput;
    public ObservableProperty<bool> IsCommand;

    public StratagemInputModel()
    {
        CommandInput = new(Vector2.zero);
        IsCommand = new(true);
    }
}
