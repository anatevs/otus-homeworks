using UnityEngine;

public class CollisionMechanic
{
    private readonly IAtomicValue<int> _damage;
    private readonly IAtomicVariable<bool> _isDead;

    public CollisionMechanic(IAtomicValue<int> damage, IAtomicVariable<bool> isDead)
    {
        _damage = damage;
        _isDead = isDead;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out Zombie zombie))
        {
            zombie.OnDamage?.Invoke(_damage.Value);
        }
        _isDead.Value = true;
    }
}