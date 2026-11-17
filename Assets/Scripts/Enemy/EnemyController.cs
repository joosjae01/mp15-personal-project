using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class EnemyController : MonoBehaviour
{
    [field : SerializeField] public BehaviorGraphAgent BehaviorAgent { get; private set; }
    [field : SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }
    [SerializeField] private EnemyDataSO _dataSO;
    [SerializeField] private EnemyAttackBase _attackSO;
    
    public PlayerController PlayerController { get; private set; }    
    public EnemyDataModel DataModel { get; private set; }
    public EnemyHealth EnemyHealth { get; private set; }

    private EnemyBoardController _boardController;

    [Inject]
    public void Inject(PlayerController playerController)
    {
        DataModel = new(_dataSO);
        EnemyHealth = new(this, DataModel);
        PlayerController = playerController;
        _boardController = new EnemyBoardController(DataModel, _attackSO, playerController, this);
    }
}
