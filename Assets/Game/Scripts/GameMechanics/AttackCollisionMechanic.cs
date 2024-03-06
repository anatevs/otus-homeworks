using UnityEngine;

public class AttackCollisionMechanic
{
    private readonly IAtomicEvent _onCounted;
    private readonly IAtomicEvent _onResetCounter;
    private readonly IAtomicValue<int> _damage;
        
    private IAtomicVariable<bool> _isMakingDamage;
    private Collider _colliderToAttack;

    private AtomicEvent<int> _playerOnDamage;

    public AttackCollisionMechanic(IAtomicEvent onCounted,
        IAtomicEvent onResetCounter, IAtomicValue<int> damage,
        IAtomicVariable<bool> isMakingDamage, Collider colliderToAttack)
    {
        _onCounted = onCounted;
        _onResetCounter = onResetCounter;
        _damage = damage;
        _isMakingDamage = isMakingDamage;
        _colliderToAttack = colliderToAttack;

        _playerOnDamage = _colliderToAttack.gameObject.GetComponent<Player>().OnDamage;
    }

    public void Update()
    {
        if (!_isMakingDamage.Value)
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
            _isMakingDamage.Value = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == _colliderToAttack)
        {
            _isMakingDamage.Value = false;
        }
    }

    public void OnEnable()
    {
        _onCounted.Subscribe(MakeDamage);
    }

    public void OnDisable()
    {
        _onCounted.Unsubscribe(MakeDamage);
    }

    private void MakeDamage()
    {
        _playerOnDamage.Invoke(_damage.Value);
    }
}