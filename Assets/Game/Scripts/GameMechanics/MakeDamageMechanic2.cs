public class MakeDamageMechanic2
{
    private readonly Entity _damagableEntity;
    private readonly IAtomicEvent _makeDamageEvent;
    private readonly IAtomicValue<int> _damage;

    public MakeDamageMechanic2(Entity damagableEntity, IAtomicEvent makeDamageEvent, IAtomicValue<int> damage)
    {
        _damagableEntity = damagableEntity;
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
        _damagableEntity.GetEntityComponent<OnDamageComponent>().MakeDamage(_damage.Value);
    }
}