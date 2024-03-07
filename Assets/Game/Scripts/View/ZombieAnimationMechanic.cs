using UnityEngine;

public partial class ZombieAnimationMechanic
{
    private static int State = Animator.StringToHash("State");
    private static int AttackTrigger = Animator.StringToHash("Attack");

    private readonly Animator _animator;
    private readonly IAtomicValue<bool> _isAttacking;
    private readonly IAtomicEvent _attackRequest;
    private readonly IAtomicValue<Vector3> _moveDirection;
    private readonly IAtomicVariable<bool> _isDead;

    public ZombieAnimationMechanic(Animator animator,
        IAtomicValue<bool> isAttacking,
        IAtomicEvent attackRequest,
        IAtomicValue<Vector3> moveDirection,
        IAtomicVariable<bool> isDead)
    {
        _animator = animator;
        _isAttacking = isAttacking;
        _attackRequest = attackRequest;
        _moveDirection = moveDirection;
        _isDead = isDead;
    }

    public void Update()
    {
        _animator.SetInteger(State, GetStateValue());
    }

    public void OnEnable()
    {
        _attackRequest.Subscribe(OnAttack);
    }

    public void OnDisable()
    {
        _attackRequest.Unsubscribe(OnAttack);
    }

    private int GetStateValue()
    {
        if (_isDead.Value)
        {
            return (int)ZombieAnimStates.Death;
        }

        if (_moveDirection.Value != Vector3.zero && !_isAttacking.Value)
        {
            return (int)ZombieAnimStates.Move;
        }

        return (int)ZombieAnimStates.Idle;
    }

    private void OnAttack()
    {
        _animator.SetTrigger(AttackTrigger);
    }
}