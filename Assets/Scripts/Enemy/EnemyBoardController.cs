using Unity.Behavior;
using UnityEngine.AI;

public class EnemyBoardController : BlackBoardController
{
    private PlayerController _playerController;
    private EnemyController _enemyController;
    private EnemyDataModel _dataModel;
    private NavMeshAgent _navMeshAgent;
    private EnemyAttackBase _attackBase;
    
    public EnemyBoardController(EnemyDataModel dataModel, EnemyAttackBase attackBase, PlayerController playerController, EnemyController enemyController)
    {
        _dataModel = dataModel;
        _attackBase = attackBase;
        _playerController = playerController;
        _enemyController = enemyController;
        _navMeshAgent = enemyController.NavMeshAgent;
        _agent = enemyController.BehaviorAgent;
        SetBoard();
    }

    protected override void SetBoard()
    {
        _agent.SetVariableValue<float>("DetectRange", _dataModel.DetectRange);
        _agent.SetVariableValue<float>("AttackRange", _dataModel.AttackRange);
        _agent.SetVariableValue<float>("MinRange", _dataModel.MinRange);
        _agent.SetVariableValue<float>("CoolDown", _dataModel.CoolDown);
        _agent.SetVariableValue<float>("AttackDelay", _dataModel.AttackDelay);
        _agent.SetVariableValue<float>("MoveSpeed", _dataModel.MoveSpeed);
        _agent.SetVariableValue<float>("RunDistance", _dataModel.RunDistance);
        
        _agent.SetVariableValue<PlayerController>("PlayerController", _playerController);
        _agent.SetVariableValue<EnemyAttackBase>("AttackBase", _attackBase);
        _agent.SetVariableValue<EnemyController>("EnemyController", _enemyController);
        _agent.SetVariableValue<NavMeshAgent>("NavMeshAgent", _navMeshAgent);
    }
}
