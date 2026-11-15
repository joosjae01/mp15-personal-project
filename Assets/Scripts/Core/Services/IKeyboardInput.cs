public interface IInputHandler {
    InputType Type { get; }
    public void Enable();
    public void Disable();
}
