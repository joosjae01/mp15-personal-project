using UnityEngine;

public interface IUIService
{
    public void ActiveUI(IView view);
    public void DeactiveUI(IView view);
}
