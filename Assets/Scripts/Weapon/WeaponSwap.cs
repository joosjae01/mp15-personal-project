using UnityEngine;

public class WeaponSwap
{
    private WeaponInputModel _inputModel;
    private WeaponSlot _slot;

    public WeaponSwap(WeaponInputModel inputModel, WeaponSlot slot)
    {
        _inputModel = inputModel;
        _slot = slot;
    }

    private void Swap(float value)
    {
        if (value > 0) _slot.Next();
        else if (value < 0) _slot.Prev();
    }

    public void BindEvents() => _inputModel.SwapValue.AddListener(Swap);
    public void UnBindEvents() => _inputModel.SwapValue.RemoveListener(Swap);
}
