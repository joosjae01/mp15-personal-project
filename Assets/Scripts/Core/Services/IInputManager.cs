using UnityEngine.InputSystem;

public interface IInputManager
{
    public void ConnectDevice(InputType type);
    public void ActiveStratagem(InputType type);
}
