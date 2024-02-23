using System;

public interface IAtomicVariable<T> : IAtomicValue<T>
{
    public event Action<T> OnValueChanged;
    new public T Value { get; set; }
}