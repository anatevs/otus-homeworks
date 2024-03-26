public sealed class AnimEventMechanic_OnDestroy
{
    private IAtomicEvent _shootEvent;
    private AtomicVariable<bool> _isDestoyed;
    private AnimationDispatcher _dispatcher;
    private PlayerAnimEventsNames _eventsNames;

    public AnimEventMechanic_OnDestroy(AtomicVariable<bool> isDestroyed, AnimationDispatcher dispatcher, PlayerAnimEventsNames eventsNames)
    {
        _isDestoyed = isDestroyed;
        _dispatcher = dispatcher;
    }

    public void OnEnable()
    {
        _dispatcher.OnEventRecieved += RecieveFromDispatcher;
    }

    public void OnDisable()
    {
        _dispatcher.OnEventRecieved -= RecieveFromDispatcher;
    }

    private void RecieveFromDispatcher(string eventName)
    {
        if (eventName == _eventsNames.DESTROY)
        {
            _isDestoyed.Value = true;
        }
    }
}
