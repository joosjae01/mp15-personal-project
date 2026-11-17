using UnityEngine;

public class SwitchInputModel
{
    public ObservableProperty<bool> OnCommand;

    public SwitchInputModel()
    {
        OnCommand = new(false);
    }
}
