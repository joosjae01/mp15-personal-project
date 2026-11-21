using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [Inject] private PlayerDataModel _playerData;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void RefreshBar(int amount) => _healthBar.fillAmount = (amount / _playerData.MaxHealth) * 100;

    private void BindEvents() => _playerData.CurrentHealth.AddListener(RefreshBar);
    private void UnBindEvents() => _playerData.CurrentHealth.RemoveListener(RefreshBar);
}
