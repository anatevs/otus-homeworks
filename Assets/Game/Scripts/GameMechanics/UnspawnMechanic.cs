using UnityEngine;

public class UnspawnMechanic
{
    private readonly GameObject _gameObject;
    private readonly AtomicVariable<bool> _isDeactivated;
    private readonly IAtomicAction<GameObject> _OnUnspawn;

    public UnspawnMechanic(GameObject gameObject, AtomicVariable<bool> isDeactivated, IAtomicAction<GameObject> OnUnspawn)
    {
        _gameObject = gameObject;
        _isDeactivated = isDeactivated;
        _OnUnspawn = OnUnspawn;
    }

    public void OnEnable()
    {
        _isDeactivated.Subscribe(OnDestroy);
    }

    public void OnDisable()
    {
        _isDeactivated.Unsubscribe(OnDestroy);
    }

    private void OnDestroy(bool isDestroyed)
    {
        if (!isDestroyed)
        {
            return;
        }
        else
        {
            _OnUnspawn.Invoke(_gameObject);
        }
    }
}