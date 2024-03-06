using UnityEngine;

public partial class ZombieVisualMechanic
{
    private static int State = Animator.StringToHash("State");
    private static int AttackTrigger = Animator.StringToHash("Attack");

    private readonly Animator _animator;
    private readonly IAtomicEvent _attackRequest;
    private readonly IAtomicValue<Vector3> _moveDirection;
    private readonly IAtomicVariable<bool> _isDead;

    public ZombieVisualMechanic(Animator animator,
        IAtomicEvent attackRequest,
        IAtomicValue<Vector3> moveDirection,
        IAtomicVariable<bool> isDead)
    {
        _animator = animator;
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

        if (_moveDirection.Value != Vector3.zero)
        {
            return (int)ZombieAnimStates.Move;
        }

        return (int)ZombieAnimStates.Idle;
    }

    private void OnAttack()
    {
        Debug.Log("on attack anim");
        _animator.SetTrigger(AttackTrigger);
    }
}