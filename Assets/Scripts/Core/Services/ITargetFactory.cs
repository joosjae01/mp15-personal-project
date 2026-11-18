using System.Collections.Generic;

public interface ITargetFactory
{
    public Queue<ITarget> Create(List<TargetContext> contextList);
}
