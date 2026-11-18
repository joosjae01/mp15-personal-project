using UnityEngine;

public class Pistol : IWeapon
{
    private WeaponDataModel _model;
    private ISoundService _soundService;
    public WeaponDataModel Model { get => _model; }
    private LayerMask _targetLayer = LayerMask.GetMask("Enemy");

    public Pistol(WeaponContext context, ISoundService soundService)
    {
        _soundService = soundService;
        _model = new WeaponDataModel(context.DataSO);
    }

    public void Fire(WeaponController controller)
    {    
        _soundService.PlaySFX(_model.SFX);
        Transform muzzle = controller.GetMuzzlePoint();

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _model.Range, _targetLayer)){
            hit.collider.gameObject.GetComponent<EnemyController>().EnemyHealth.TakeDamage(_model.Power);
            Debug.Log(hit.collider.name);
        }
    }
}
