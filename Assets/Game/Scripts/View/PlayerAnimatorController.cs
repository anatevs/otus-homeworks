using UnityEngine;

public partial class PlayerAnimatorController
{
    private static int State = Animator.StringToHash("State");

    private IAtomicValue<Vector3> _moveDirection;
    private IAtomicValue<bool> _isDead;
    private Animator _animator;

    public PlayerAnimatorController(IAtomicValue<Vector3> moveDirection, IAtomicValue<bool> isDead, Animator animator)
    {
        _moveDirection = moveDirection;
        _isDead = isDead;
        _animator = animator;
    }

    public void Update()
    {
        _animator.SetInteger(State, GetStateValue());
    }

    private int GetStateValue()
    {
        if (_isDead.Value)
        {
            return (int)PlayerAnimStates.Dead;
        }

        if (_moveDirection.Value != Vector3.zero)
        {
            return (int)PlayerAnimStates.Move;
        }

        return (int)PlayerAnimStates.Idle;
    }
}