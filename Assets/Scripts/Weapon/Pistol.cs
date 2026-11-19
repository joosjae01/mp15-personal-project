using UnityEngine;

public class Pistol : IWeapon
{
    public WeaponDataModel Model { get; private set; }
    private ISoundService _soundService;
    
    private LayerMask _targetLayer = LayerMask.GetMask("Enemy");

    public Pistol(WeaponContext context, ISoundService soundService)
    {
        _soundService = soundService;
        Model = new WeaponDataModel(context.DataSO);
    }

    public void Fire(WeaponController controller)
    {    
        _soundService.PlaySFX(Model.SFX);
        Transform muzzle = controller.GetMuzzlePoint();

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Model.Range, _targetLayer)){
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(Model.Power);
        }
    }
}
