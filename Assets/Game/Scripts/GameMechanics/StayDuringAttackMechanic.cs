

using UnityEngine;

public class StayDuringAttackMechanic
{
    private readonly AtomicVariable<bool> _isAttacking;
    private readonly IAtomicVariable<bool> _canMove;
    private readonly IAtomicValue<bool> _isDead;

    public StayDuringAttackMechanic(AtomicVariable<bool> isAttacking,
        IAtomicVariable<bool> canMove, IAtomicValue<bool> isDead)
    {
        _isAttacking = isAttacking;
        _canMove = canMove;
        _isDead = isDead;
    }

    public void OnEnable()
    {
        _isAttacking.Subscribe(MakeStop);
    }

    public void OnDisable()
    {
        _isAttacking.Unsubscribe(MakeStop);
    }

    private void MakeStop(bool isAttacking)
    {
        if (_isDead.Value)
        {
            return;
        }
        else
        {
            _canMove.Value = !isAttacking;
        }
    }
}