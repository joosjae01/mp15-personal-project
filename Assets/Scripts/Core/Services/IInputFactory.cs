using System.Collections.Generic;

public interface IInputFactory
{
    public List<IInputHandler> Create(InputContext context);
}
