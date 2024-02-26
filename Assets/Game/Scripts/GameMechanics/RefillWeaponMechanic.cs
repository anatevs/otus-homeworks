public class RefillWeaponMechanic
{
    private readonly IAtomicEvent _canRefillEvent;
    private readonly IAtomicVariable<int> _projectileStorage;
    private readonly IAtomicValue<int> _refillAmout;

    public RefillWeaponMechanic(IAtomicEvent canRefillEvent, IAtomicVariable<int> projectileStorage, IAtomicValue<int> refillAmout)
    {
        this._canRefillEvent = canRefillEvent;
        _projectileStorage = projectileStorage;
        _refillAmout = refillAmout;
    }

    public void OnEnable()
    {
        _canRefillEvent.Subscribe(MakeWeaponRefill);
    }

    public void OnDisable()
    {
        _canRefillEvent.Unsubscribe(MakeWeaponRefill);
    }

    private void MakeWeaponRefill()
    {
        _projectileStorage.Value += _refillAmout.Value;
    }
}