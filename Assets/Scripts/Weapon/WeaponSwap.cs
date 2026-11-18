using System.Threading.Tasks;
using UnityEngine;

public class WeaponSwap
{
    private WeaponInputModel _inputModel;
    private WeaponSlot _slot;
    private bool _canSwap;

    public WeaponSwap(WeaponInputModel inputModel, WeaponSlot slot)
    {
        _inputModel = inputModel;
        _slot = slot;
        _canSwap = true;
    }

    private void Swap(float value)
    {
        if (!_canSwap || value == 0) return;

        if (value > 0) _slot.Next();
        else if (value < 0) _slot.Prev();
    }

    public void BindEvents() => _inputModel.SwapValue.AddListener(Swap);
    public void UnBindEvents() => _inputModel.SwapValue.RemoveListener(Swap);
}
