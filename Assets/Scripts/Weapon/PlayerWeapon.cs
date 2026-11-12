using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerWeapon : MonoBehaviour
{
    [Inject] private ISwapService _swapWeapon;
    private List<IWeapon> _weaponList;
    private IWeapon _currentWeapon;
    
    private void Fire()
    {
        _currentWeapon.Fire();
    }

    private void Init()
    {
        _currentWeapon = _weaponList[0];
    }
}
