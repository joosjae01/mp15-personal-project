using System;

public interface IView
{
    public void Show();
    public void Hide();
    public void AddListener(Action<bool> action);
    public void RemoveListener(Action<bool> action);
}
