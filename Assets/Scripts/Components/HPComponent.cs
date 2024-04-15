using UnityEngine;

public struct HPComponent : IComponent
{
    public int Value
    {
        get => _value;
        set => _value = Mathf.Clamp(value, 0, _initValue);
    }

    private int _value;
    private readonly int _initValue;

    public HPComponent(int value)
    {
        _value = value;
        _initValue = value;
    }
}