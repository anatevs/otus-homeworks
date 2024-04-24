using UnityEngine;

public struct HPComponent : IComponent
{
    public int CurrentHP
    {
        get => _value;
        set => _value = Mathf.Max(0, value);
    }

    public int InitHP => _initValue;

    private int _value;
    private readonly int _initValue;

    public HPComponent(int value)
    {
        _value = value;
        _initValue = value;
    }
}