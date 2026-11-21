using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Pool;
using VContainer;

public class EnemyController : MonoBehaviour, IPoolable
{
    [field : SerializeField] public BehaviorGraphAgent BehaviorAgent { get; private set; }
    [field : SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }

    [SerializeField] private EnemyDataSO _dataSO;
    [SerializeField] private EnemyAttackBase _attackSO;
    [SerializeField] private EnemyHitEffect _enemyHitEffect;
    
    public PlayerController PlayerController { get; private set; }    
    public EnemyDataModel DataModel { get; private set; }
    public ObjectPool<IPoolable> Source { get; private set; }
    public IDamageable EnemyHealth { get; private set; }
    public GameObject GameObject { get => gameObject; }
    
    private EnemyBoardController _boardController;
    private IPoolService _poolService;

    [Inject]
    public void Inject(PlayerController playerController, IPoolService poolService)
    {
        _poolService = poolService;
        DataModel = new(_dataSO);
        PlayerController = playerController;
        EnemyHealth = new EnemyHealth(this, DataModel);
        _boardController = new EnemyBoardController(DataModel, _attackSO, playerController, this);
        NavMeshAgent.enabled = false;
        _enemyHitEffect.Inject(poolService, DataModel);
    }

    private void OnDisable() => ResetData();

    private void ResetData()
    {
        Animator.SetBool("IsDead", false);
        BehaviorAgent.SetVariableValue<bool>("IsDead", false);
        DataModel.Reset(_dataSO);
        DataModel.IsKilled.RemoveAllListener();
    }

    public void TakeDamage(float damage) => EnemyHealth.TakeDamage(damage);

    public void SetSource(ObjectPool<IPoolable> source)
    {
        Source = source;
    }
}
