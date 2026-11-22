using UnityEngine;
using VContainer;

public class PlayerRotate : MonoBehaviour
{
    private PlayerInputModel _inputModel;
    private IPauseService _pause;

    [Inject]
    public void Inject(PlayerInputModel inputModel, IPauseService pause)
    {
        _pause = pause;
        _inputModel = inputModel;
    }

    private void Start() => BindEvents();
    private void OnDestroy() => UnBindEvents();

    private void Look(Vector3 lookInput)
    {
        if (_pause.IsPaused) return;

        lookInput.y = 0;
        transform.LookAt(lookInput);
    }

    private void BindEvents() => _inputModel.LookDirection.AddListener(Look);
    private void UnBindEvents() => _inputModel.LookDirection.RemoveListener(Look);
}
