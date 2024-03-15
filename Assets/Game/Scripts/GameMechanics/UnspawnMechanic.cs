using UnityEngine;

public class UnspawnMechanic
{
    private readonly GameObject _gameObject;
    private readonly AtomicVariable<bool> _isDeactivated;
    private readonly IAtomicVariable<bool> _isAttacking;
    private readonly IAtomicVariable<bool> _canMove;
    private readonly IAtomicVariable<int> _hp;
    private readonly int _initHP;
    private readonly IAtomicVariable<bool> _isDead;
    private readonly IAtomicAction<GameObject> _OnUnspawn;

    public UnspawnMechanic(GameObject gameObject, 
        AtomicVariable<bool> isDeactivated,
        IAtomicVariable<bool> isAttacking,
        IAtomicVariable<bool> canMove,
        IAtomicVariable<int> hp,
        int initHP,
        IAtomicVariable<bool> isDead,
        IAtomicAction<GameObject> OnUnspawn)
    {
        _gameObject = gameObject;
        _isDeactivated = isDeactivated;
        _isAttacking = isAttacking;
        _canMove = canMove;
        _OnUnspawn = OnUnspawn;
        _hp = hp;
        _initHP = initHP;
        _isDead = isDead;
    }

    public void OnEnable()
    {
        _isDeactivated.Subscribe(OnDeactivate);
    }

    public void OnDisable()
    {
        _isDeactivated.Unsubscribe(OnDeactivate);
    }

    private void OnDeactivate(bool isDeactivated)
    {
        if (!isDeactivated)
        {
            return;
        }
        else
        {
            _isAttacking.Value = false;
            _canMove.Value = true;
            _hp.Value = _initHP;
            _OnUnspawn.Invoke(_gameObject);
            _isDead.Value = false;
        }
    }
}