using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BasicInputAction", menuName = "SO/InputAction/BasicInputAction")]
public class BasicInputAction : ScriptableObject
{
    public InputActionReference Move;
    public InputActionReference Look;
    public InputActionReference Swap;
    public InputActionReference Fire;
    public InputActionReference Reload;
    public InputActionReference Stratagem;
}
