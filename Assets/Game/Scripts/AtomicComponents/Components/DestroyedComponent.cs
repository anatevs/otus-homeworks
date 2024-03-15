using System;

public class DestroyedComponent : IDestroyedComponent
{
    public event Action<bool> OnDestroyed
    {
        add { _onDestroy.Subscribe(value); }
        remove { _onDestroy.Unsubscribe(value); }
    }

    private readonly AtomicVariable<bool> _onDestroy;

    public DestroyedComponent(AtomicVariable<bool> onDestroy)
    {
        _onDestroy = onDestroy;
    }
}