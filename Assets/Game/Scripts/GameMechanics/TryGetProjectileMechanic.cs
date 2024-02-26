public class TryGetProjectileMechanic
{
    private readonly IAtomicEvent _fireEvent;
    private readonly IAtomicAction _shootAction;
    private readonly IAtomicVariable<int> _weaponMagazine;
    private readonly int _shootAmount;

    public TryGetProjectileMechanic(IAtomicEvent onFireEvent, IAtomicAction shootAction, IAtomicVariable<int> weaponMagazine)
    {
        _fireEvent = onFireEvent;
        _shootAction = shootAction;
        _weaponMagazine = weaponMagazine;
        _shootAmount = 1;
    }

    public void OnEnable()
    {
        _fireEvent.Subscribe(MakeShoot);
    }

    public void OnDisable()
    {
        _fireEvent.Unsubscribe(MakeShoot);
    }

    private void MakeShoot()
    {
        if (_weaponMagazine.Value <= 0)
        {
            return;
        }
        else
        {
            _weaponMagazine.Value -= _shootAmount;
            _shootAction.Invoke();
        }
    }
}