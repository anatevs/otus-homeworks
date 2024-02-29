public class LifetimeMechanic
{
    private readonly IAtomicEvent _onLifetimeEnd;
    private readonly IAtomicVariable<bool> _isDead;

    public LifetimeMechanic(IAtomicEvent onLifetimeEnd, IAtomicVariable<bool> isDead)
    {
        _onLifetimeEnd = onLifetimeEnd;
        _isDead = isDead;
    }

    public void OnEnable()
    {
        _onLifetimeEnd.Subscribe(MakeDeath);
    }

    public void OnDisable()
    {
        _onLifetimeEnd.Unsubscribe(MakeDeath);
    }

    private void MakeDeath()
    {
        _isDead.Value = true;
    }
}