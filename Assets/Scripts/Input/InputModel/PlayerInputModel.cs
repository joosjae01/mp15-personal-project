using UnityEngine;

public class PlayerInputModel
{
    public ObservableProperty<Vector2> MoveDirection;
    public ObservableProperty<Vector3> LookDirection;
    public ObservableProperty<bool> IsMoving;
    public ObservableProperty<bool> OnInteract;

    public PlayerInputModel()
    {
        MoveDirection = new(Vector2.zero);
        LookDirection = new(Vector3.zero);
        IsMoving = new(false);
        OnInteract = new(false);
    }
}
