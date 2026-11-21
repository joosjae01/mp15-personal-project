using UnityEngine;

public class UIInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string _desc = "E - 열기";
    [SerializeField] private GameObject _target;   // 열 UI

    public string Desc => _desc;
    public void Interact() => _target.GetComponent<IView>().Show();
}