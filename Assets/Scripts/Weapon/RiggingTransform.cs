using UnityEngine;

public class RiggingTransform : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    public Transform GetLeftHand()
    {
        return _leftHand;
    }

    public Transform GetRightHand()
    {
        return _rightHand;
    }
}
