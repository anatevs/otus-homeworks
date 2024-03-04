using UnityEngine;

public partial class PlayerAnimatorController
{
    private static int State = Animator.StringToHash("MainState");

    private static int ShootTrigger = Animator.StringToHash("Shoot");
    private static int TakeDamageTrigger = Animator.StringToHash("TakeDamage");

    private IAtomicValue<Vector3> _moveDirection;
    private IAtomicValue<bool> _isDead;
    private Animator _animator;

    private IAtomicEvent<int> _onDamage;
    private IAtomicEvent _shootEvent;

    public PlayerAnimatorController(IAtomicValue<Vector3> moveDirection, IAtomicValue<bool> isDead, Animator animator, IAtomicEvent<int> onDamage, IAtomicEvent shootEvent)
    {
        _moveDirection = moveDirection;
        _isDead = isDead;
        _animator = animator;
        _onDamage = onDamage;
        _shootEvent = shootEvent;
    }

    public void Update()
    {
        _animator.SetInteger(State, GetStateValue());
    }

    public void OnEnable()
    {
        _onDamage.Subscribe(TakeDamage);
        _shootEvent.Subscribe(MakeShoot);
    }

    public void OnDisable()
    {
        _onDamage.Unsubscribe(TakeDamage);
        _shootEvent.Unsubscribe(MakeShoot);
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
        Debug.Log($"shoot animation {Time.time}");
    }

    private void TakeDamage(int _)
    {
        _animator.SetTrigger(TakeDamageTrigger);
        Debug.Log("damage");
    }

}