using System;

public class OnDamageComponent : IOnDamageComponent
{
    private readonly IAtomicEvent<int> _onDamage;

    public OnDamageComponent(IAtomicEvent<int> onDamage)
    {
        _onDamage = onDamage;
    }

    public void MakeDamage(int damage)
    {
        _onDamage?.Invoke(damage);
    }
}