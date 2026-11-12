using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "StratagemInputAction", menuName = "SO/InputAction/StratagemInputAction")]
public class StratagemInputAction : ScriptableObject
{
    public InputActionReference Command;
    public InputActionReference Quit;
}
