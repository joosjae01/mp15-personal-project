using System.Collections.Generic;
using VContainer;

public class TargetFactory : ITargetFactory
{
    private PlayerDataModel _playerData;
    private EnemySpawner _spawner;

    [Inject]
    public TargetFactory(PlayerDataModel playerData, EnemySpawner spawner)
    {
        _playerData = playerData;
        _spawner = spawner;
    }

    public Queue<ITarget> Create(List<TargetContext> contextList)
    {
        Queue<ITarget> targetQueue = new();

        foreach(TargetContext context in contextList)
        {
            context.PlayerData = _playerData;
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
