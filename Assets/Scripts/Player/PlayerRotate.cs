using UnityEngine;
using VContainer;

public class PlayerRotate : MonoBehaviour
{
    private PlayerInputModel _inputModel;
    private Vector3 _look;

    [Inject]
    public void Inject(PlayerInputModel inputModel) => _inputModel = inputModel;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void Look(Vector3 lookInput)
    {
        lookInput.y = 0;
        transform.LookAt(lookInput);
    }

    private void BindEvents() => _inputModel.LookDirection.AddListener(Look);
    private void UnBindEvents() => _inputModel.LookDirection.RemoveListener(Look);
}
