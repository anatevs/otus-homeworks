using System;
using UnityEngine;

public class UnspawnComponent : IUnspawnComponent
{
    public event Action<GameObject> OnUnspawn
    {
        add { _onUnspawn.Subscribe(value); }
        remove { _onUnspawn.Unsubscribe(value); }
    }

    private readonly AtomicEvent<GameObject> _onUnspawn;

    public UnspawnComponent(AtomicEvent<GameObject> onUnspawn)
    {
        _onUnspawn = onUnspawn;
    }
}