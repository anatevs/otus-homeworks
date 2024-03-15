using UnityEngine;

public class AttackCollisionMechanic
{
    private readonly IAtomicEvent _onResetCounter;
    private readonly IAtomicVariable<bool> _isAttacking;
    private readonly Collider _colliderToAttack;
    private readonly IAtomicValue<bool> _isGameFinished;

    public AttackCollisionMechanic(IAtomicEvent onResetCounter,
        IAtomicVariable<bool> isAttacking, 
        Collider colliderToAttack,
        IAtomicValue<bool> isGameFinished)
    {
        _onResetCounter = onResetCounter;
        _isAttacking = isAttacking;
        _colliderToAttack = colliderToAttack;
        _isGameFinished = isGameFinished;
    }

    public void Update()
    {
        if (_isGameFinished.Value)
        {
            _isAttacking.Value = false;
            return;
        }

        if (!_isAttacking.Value)
        {
            _onResetCounter.Invoke();
        }
        else
        {
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_isGameFinished.Value)
        {
            return;
        }

        if (other == _colliderToAttack)
        {
            _isAttacking.Value = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == _colliderToAttack)
        {
            _isAttacking.Value = false;
        }
    }
}