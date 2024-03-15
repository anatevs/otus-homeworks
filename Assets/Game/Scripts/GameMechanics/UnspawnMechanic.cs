using UnityEngine;

public class UnspawnMechanic
{
    private readonly GameObject _gameObject;
    private readonly AtomicVariable<bool> _isDeactivated;
    private readonly IAtomicVariable<bool> _isAttacking;
    private readonly IAtomicAction<GameObject> _OnUnspawn;

    public UnspawnMechanic(GameObject gameObject, 
        AtomicVariable<bool> isDeactivated,
        IAtomicVariable<bool> isAttacking,
        IAtomicAction<GameObject> OnUnspawn)
    {
        _gameObject = gameObject;
        _isDeactivated = isDeactivated;
        _isAttacking = isAttacking;
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
            _isAttacking.Value = false;
            _OnUnspawn.Invoke(_gameObject);
        }
    }
}