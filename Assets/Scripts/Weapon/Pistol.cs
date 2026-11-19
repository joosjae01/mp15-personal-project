using UnityEngine;

public class Pistol : IWeapon
{
    public WeaponDataModel Model { get; private set; }
    private ISoundService _soundService;
    private IPoolService _poolService;
    private Transform _muzzlePoint;
    private Vector3 _direction;
    private LayerMask _targetLayer = LayerMask.GetMask("Enemy");

    public Pistol(WeaponContext context, ISoundService soundService, IPoolService poolService)
    {
        _soundService = soundService;
        _poolService = poolService;
        Model = new WeaponDataModel(context.DataSO);
    }

    public void Fire(WeaponController controller)
    {
        _soundService.PlaySFX(Model.SFX);
        _muzzlePoint = controller.GetMuzzlePoint();
        _direction = _muzzlePoint.forward;
        _direction += new Vector3(
            Random.Range(-Model.Stability, Model.Stability),
            0.0f,
            Random.Range(-Model.Stability, Model.Stability)
            );

        _direction = _direction.normalized;

        Ray ray = new Ray(_muzzlePoint.position, _direction);
        IPoolable effect = _poolService.Take(Model.VFX);
        effect.GameObject.transform.SetPositionAndRotation(_muzzlePoint.position, Quaternion.Euler(_direction));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Model.Range, _targetLayer))
        {
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(Model.Power);
            (effect as IBullet).SetTarget(hit.point);
        }
        else
        {
            (effect as IBullet).SetTarget(_muzzlePoint.position + _direction * Model.Range);
        }
    }
}
