using System.Collections.Generic;
using VContainer;

public class TargetFactory : ITargetFactory
{
    private PlayerController _controller;
    private PlayerDataModel _playerData;
    private ISpawner _spawner;

    [Inject]
    public TargetFactory(PlayerController controller, PlayerDataModel playerData, ISpawner spawner)
    {
        _controller = controller;
        _playerData = playerData;
        _spawner = spawner;
    }

    public Queue<ITarget> Create(List<TargetContext> contextList)
    {
        Queue<ITarget> targetQueue = new();

        foreach (TargetContext origin in contextList)
        {
            TargetContext context = origin;
            context.PlayerData = _playerData;
            context.PlayerController = _controller;
            context.Spawner = _spawner;

            ITarget target = context.Type switch
            {
                TargetType.Extermination => new Extermination(context),
                TargetType.Escape => new Escape(context),
                _ => null
            };

            targetQueue.Enqueue(target);
        }

        return targetQueue;
    }
}
