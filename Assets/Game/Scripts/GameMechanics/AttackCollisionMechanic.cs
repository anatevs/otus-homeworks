using UnityEngine;

public class AttackCollisionMechanic
{
    private readonly IAtomicEvent _onResetCounter;
    private IAtomicVariable<bool> _isMakingDamage;
    private Collider _colliderToAttack;

    public AttackCollisionMechanic(IAtomicEvent onResetCounter,
        IAtomicVariable<bool> isMakingDamage, Collider colliderToAttack)
    {
        _onResetCounter = onResetCounter;
        _isMakingDamage = isMakingDamage;
        _colliderToAttack = colliderToAttack;
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
}