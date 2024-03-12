using System;

public sealed class DeathComponent : IDeathComponent
{
    public event Action<bool> OnDeath
    {
        add { _isDeath.Subscribe(value); }
        remove { _isDeath.Unsubscribe(value); }
    }

    private readonly AtomicVariable<bool> _isDeath;

    public DeathComponent(AtomicVariable<bool> isDeath)
    {
        _isDeath = isDeath;
    }

    public void SetIsDeath(bool isDeath)
    {
        _isDeath.Value = isDeath;
    }
}