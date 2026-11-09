using System;

public class ObservableProperty<T>
{
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    private event Action<T> OnValueChanged;

    public ObservableProperty(T value)
    {
        _value = value;
    }

    public void AddListener(Action<T> action) => OnValueChanged += action;
    public void RemoveListener(Action<T> action) => OnValueChanged -= action;
    public void RemoveAllListener() => OnValueChanged = null;
}
