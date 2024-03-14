using System;

public sealed class DeathComponent : IDeathComponent
{
    public event Action<bool> OnDeath
    {
        add { _isDead.Subscribe(value); }
        remove { _isDead.Unsubscribe(value); }
    }

    public bool IsDead
    {
        get => _isDead.Value;
        set => _isDead.Value = value;
    }

    private readonly AtomicVariable<bool> _isDead;

    public DeathComponent(AtomicVariable<bool> isDeath)
    {
        _isDead = isDeath;
    }
}