using UnityEngine;

public class CommandArrow : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    public void SetDirection(Command cmd)
    {
        float z = cmd switch
        {
            Command.UP => 0.0f,
            Command.LEFT => 90.0f,
            Command.DOWN => 180.0f,
            Command.RIGHT => -90.0f,
            _ => 0.0f
        };

        transform.localRotation = Quaternion.Euler(0, 0, z);
    }

    public void SetActive(bool value) => _prefab.SetActive(value);
}
