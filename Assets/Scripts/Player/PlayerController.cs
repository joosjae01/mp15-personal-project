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
}
