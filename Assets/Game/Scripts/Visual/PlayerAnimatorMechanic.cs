using UnityEngine;

public sealed class PlayerAnimatorMechanic
{
    private readonly static int State = Animator.StringToHash("MainState");

    private readonly static int ShootTrigger = Animator.StringToHash("Shoot");
    private readonly static int TakeDamageTrigger = Animator.StringToHash("TakeDamage");

    private readonly Animator _animator;
    private readonly IAtomicValue<Vector3> _moveDirection;
    private readonly IAtomicValue<bool> _isDead;
    private readonly IAtomicEvent<int> _onDamage;
    private readonly IAtomicEvent _fireRequest;

    public PlayerAnimatorMechanic(Animator animator,
        IAtomicValue<Vector3> moveDirection, IAtomicValue<bool> isDead,
        IAtomicEvent<int> onDamage, IAtomicEvent fireRequest)
    {
        _animator = animator;
        _moveDirection = moveDirection;
        _isDead = isDead;
        _onDamage = onDamage;
        _fireRequest = fireRequest;
    }

    public void Update()
    {
        _animator.SetInteger(State, GetStateValue());
    }

    public void OnEnable()
    {
        _onDamage.Subscribe(TakeDamage);
        _fireRequest.Subscribe(MakeShoot);
    }

    public void OnDisable()
    {
        _onDamage.Unsubscribe(TakeDamage);
        _fireRequest.Unsubscribe(MakeShoot);
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

    private void MakeShoot()
    {
        _animator.SetTrigger(ShootTrigger);
    }

    private void TakeDamage(int _)
    {
        _animator.SetTrigger(TakeDamageTrigger);
    }

}