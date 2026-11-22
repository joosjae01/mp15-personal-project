using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [Inject] public PlayerAnimation Animation { get; private set; }
    [Inject] public PlayerMovement Movement { get; private set; }
    [Inject] public PlayerCamera Camera { get; private set; }
    [Inject] public PlayerRotate Rotation { get; private set; }
    [Inject] public PlayerDataModel DataModel { get; private set; }
    [Inject] public PlayerHealth Health { get; private set; }
    [Inject] public PlayerWeaponRigger Rigger { get; private set; }
    [Inject] private WeaponController _weaponController;

    [SerializeField] private float _forwardForce;
    [SerializeField] private float _upForce;

    private EscapeBeacon _beacon;
    private Escape _escape;

    public bool HasBeacon => _beacon != null;

    public void SetEscape(Escape escape)
    {
        _escape = escape;
    }

    public void HoldBeacon(GameObject beacon)
    {
        _beacon = beacon.GetComponent<EscapeBeacon>();
        GameObject curr = _weaponController.GetWeaponSlot().CurrentWeapon.Model.Prefab;
        Rigger.DeactiveWeapon(curr);
        Rigger.HoldThrown(_beacon.gameObject, _beacon.RightHand);
    }

    public void ThrowBeacon()
    {
        if (_beacon == null || _escape == null) return;

        _beacon.transform.SetParent(null);
        Vector3 force = transform.forward * _forwardForce + Vector3.up * _upForce;
        _beacon.Throw(force, _escape.OnBeacon);
        _beacon = null;

        GameObject curr = _weaponController.GetWeaponSlot().CurrentWeapon.Model.Prefab;
        Rigger.ActiveWeapon(curr);
        Rigger.ApplyRig(curr);
    }
}
