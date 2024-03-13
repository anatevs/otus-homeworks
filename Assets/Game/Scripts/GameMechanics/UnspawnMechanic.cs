using UnityEngine;

public class UnspawnMechanic
{
    private readonly GameObject _gameObject;
    private readonly AtomicVariable<bool> _isDestroyed;
    private readonly IAtomicAction<GameObject> _OnUnspawn;

    public UnspawnMechanic(GameObject gameObject, AtomicVariable<bool> isDestroyed, IAtomicAction<GameObject> OnUnspawn)
    {
        _gameObject = gameObject;
        _isDestroyed = isDestroyed;
        _OnUnspawn = OnUnspawn;
    }

    public void OnEnable()
    {
        _isDestroyed.Subscribe(OnDestroy);
    }

    public void OnDisable()
    {
        _isDestroyed.Unsubscribe(OnDestroy);
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