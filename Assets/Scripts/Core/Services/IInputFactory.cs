public interface IInputFactory
{
    public IInputHandler CreateKeyboard(InputType type);
    public IInputHandler CreateGamePad(InputType type);
}
