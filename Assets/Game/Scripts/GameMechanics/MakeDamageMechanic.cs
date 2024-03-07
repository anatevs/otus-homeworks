public class MakeDamageMechanic
{
    private IAtomicEvent<int> _damagableOnDamage;
    private IAtomicEvent _makeDamageEvent;
    private IAtomicValue<int> _damage;

    public MakeDamageMechanic(IAtomicEvent<int> damagableOnDamage, IAtomicEvent makeDamageEvent, IAtomicValue<int> damage)
    {
        _damagableOnDamage = damagableOnDamage;
        _makeDamageEvent = makeDamageEvent;
        _damage = damage;
    }

    public void OnEnable()
    {
        _makeDamageEvent.Subscribe(MakeDamage);
    }

    public void OnDisable()
    {
        _makeDamageEvent.Unsubscribe(MakeDamage);
    }

    private void MakeDamage()
    {
        _damagableOnDamage.Invoke(_damage.Value);
    }
}