

using UnityEngine;

public class StayDuringAttackMechanic
{
    private AtomicVariable<bool> _isAttacking;
    private IAtomicVariable<bool> _canMove;

    public StayDuringAttackMechanic(AtomicVariable<bool> isAttacking, IAtomicVariable<bool> canMove)
    {
        _isAttacking = isAttacking;
        _canMove = canMove;
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
        _canMove.Value = !isAttacking;
    }
}