using UnityEngine;

public class ShotGun : IWeapon
{
    private WeaponDataModel _model;
    private ISoundService _soundService;
    private IPoolService _poolService;
    private Transform _muzzlePoint;
    private LayerMask _targetLayer = LayerMask.GetMask("Enemy");
    public WeaponDataModel Model { get => _model; }

    private int _pellet = 8;

    public ShotGun(WeaponContext context, ISoundService soundService, IPoolService poolService)
    {
        _poolService = poolService;
        _soundService = soundService;
        _model = new WeaponDataModel(context.DataSO);
    }

    public void Fire(WeaponController controller)
    {
        _soundService.PlaySFX(_model.SFX);
        _muzzlePoint = controller.GetMuzzlePoint();

        for(int i = 0; i < _pellet; i++)
        {
            FirePellet();
        }
    }

    private void FirePellet()
    {
        Vector3 direction = _muzzlePoint.forward;
        direction += new Vector3(
            Random.Range(-Model.Stability, Model.Stability),
            Random.Range(-Model.Stability * 0.5f, Model.Stability * 0.5f),
            Random.Range(-Model.Stability, Model.Stability)
            );
        direction = direction.normalized;

        Ray ray = new Ray(_muzzlePoint.position, direction);
        IPoolable effect = _poolService.Take(Model.VFX);
        effect.GameObject.transform.SetPositionAndRotation(_muzzlePoint.position, Quaternion.LookRotation(direction));

        if (Physics.Raycast(ray, out RaycastHit hit, Model.Range, _targetLayer))
        {
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(Model.Power);
            (effect as IBullet).SetTarget(hit.point);
        }
        else
        {
            (effect as IBullet).SetTarget(_muzzlePoint.position + direction * Model.Range);
        }
    }
}
