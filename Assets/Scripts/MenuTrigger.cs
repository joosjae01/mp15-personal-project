using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
