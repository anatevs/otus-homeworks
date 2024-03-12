using System;

public sealed class HPComponent : IHPComponent
{
    public event Action<int> OnHPChanged
    {
        add { _hp.Subscribe(value); }
        remove { _hp.Unsubscribe(value); }
    }

    public int HP 
    {
        get => _hp.Value;
        set => _hp.Value = value;
    }

    private readonly AtomicVariable<int> _hp;

    public HPComponent(AtomicVariable<int> hp)
    {
        _hp = hp;
    }
}