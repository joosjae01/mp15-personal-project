using UnityEngine;
using VContainer;

public class MockPlayerSwapWeapon : ISwapService
{
    private PlayerInputModel _inputModel;
    private float _val;

    public void Swap(float axis)
    {
        if(axis >= 1.0f)
        {
            _val += Time.deltaTime;
            if(_val >= 0.05f)
            {
                _val = 0.0f;
                Debug.Log($"다음 무기 변경 : {_val}");
            }
        }
        else if(axis <= -1.0f)
        {
            _val -= Time.deltaTime;
            if(_val <= -0.05f)
            {
                _val = 0.0f;
                Debug.Log($"이전 무기로 변경 : {_val}");
            }
        }        
    }

    [Inject]
    public MockPlayerSwapWeapon(PlayerInputModel inputModel)
    {
        _inputModel = inputModel;
        //_inputModel.SwapValue.AddListener(Swap);
    }
}
