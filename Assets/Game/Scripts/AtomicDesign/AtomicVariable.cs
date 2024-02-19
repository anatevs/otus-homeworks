using System;
using UnityEngine;

[Serializable]
public class AtomicVariable<T>
{
    private event Action<T> OnValueChanged;

    [SerializeField]
    private T _value;

    public T Value 
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    public void Subscribe(Action<T> action)
    {
        OnValueChanged += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        OnValueChanged -= action;
    }
}