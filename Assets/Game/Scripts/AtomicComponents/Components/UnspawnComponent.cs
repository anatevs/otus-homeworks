using System;

public class UnspawnComponent : IUnspawnComponent
{
    public event Action<Entity> OnUnspawn
    {
        add { _onUnspawn.Subscribe(value); }
        remove { _onUnspawn.Unsubscribe(value); }
    }

    private readonly AtomicEvent<Entity> _onUnspawn;

    public UnspawnComponent(AtomicEvent<Entity> onUnspawn)
    {
        _onUnspawn = onUnspawn;
    }
}