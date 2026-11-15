using UnityEngine.InputSystem;

public interface IInputManager
{
    public void ConnectDevice(InputDevice device);
    public void ActiveStratagem(bool active);
}
