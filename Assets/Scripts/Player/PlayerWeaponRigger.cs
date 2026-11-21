using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponRigger : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint _leftHand;
    [SerializeField] private TwoBoneIKConstraint _rightHand;
    [SerializeField] private RigBuilder _rig;
    [SerializeField] private Transform _stratagemPivot;
    private Dictionary<GameObject, RiggingTransform> _rigDict = new();
    private Dictionary<GameObject, GameObject> _stratagemDict = new();

    public void CreateWeapon(GameObject prefab)
    {
        GameObject weapon = Instantiate(prefab, _stratagemPivot);
        weapon.SetActive(false);
        _stratagemDict.Add(prefab, weapon);
        _rigDict.Add(prefab, weapon.GetComponent<RiggingTransform>());
    }

    public void ActiveWeapon(GameObject prefab)
    {
        _stratagemDict[prefab].SetActive(true);
    }

    public void DeactiveWeapon(GameObject prefab)
    {
        _stratagemDict[prefab].SetActive(false);
    }

    public void ApplyRig(GameObject prefab)
    {
        if (!_rigDict.ContainsKey(prefab)) return;

        Transform left = _rigDict[prefab].GetLeftHand();
        Transform right = _rigDict[prefab].GetRightHand();

        _leftHand.data.target = left;
        _rightHand.data.target = right;
        _rig.Build();
    }

    public void DisApplyRig()
    {
        _leftHand.data.target = null;
        _rightHand.data.target = null;
        _rig.Build();
    }
}
