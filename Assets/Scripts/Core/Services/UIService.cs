using UnityEngine;

public class UIService : MonoBehaviour, IUIService
{
    [SerializeField] private GameObject _weaponSelection;
    [SerializeField] private GameObject _stratagemSelection;
    [SerializeField] private GameObject _enterMission;

    public void ActiveUI(IView view)
    {
        view.Show();
    }

    public void DeactiveUI(IView view)
    {
        view.Hide();
    }
}
