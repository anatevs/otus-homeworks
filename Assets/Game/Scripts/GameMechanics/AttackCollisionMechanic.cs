using UnityEngine;

public class AttackCollisionMechanic
{
    private readonly IAtomicEvent _onResetCounter;
    private IAtomicVariable<bool> _isAttacking;
    private readonly Collider _colliderToAttack;

    public AttackCollisionMechanic(IAtomicEvent onResetCounter,
        IAtomicVariable<bool> isAttacking, Collider colliderToAttack)
    {
        _onResetCounter = onResetCounter;
        _isAttacking = isAttacking;
        _colliderToAttack = colliderToAttack;
    }

    public void Update()
    {
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