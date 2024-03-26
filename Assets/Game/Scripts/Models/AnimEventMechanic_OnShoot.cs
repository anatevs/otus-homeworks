public sealed class AnimEventMechanic_OnShoot
{
    private IAtomicEvent _shootEvent;
    private AnimationDispatcher _dispatcher;
    private PlayerAnimEventsNames _eventsNames;

    public AnimEventMechanic_OnShoot(IAtomicEvent shootEvent, AnimationDispatcher dispatcher, PlayerAnimEventsNames eventsNames)
    {
        _shootEvent = shootEvent;
        _dispatcher = dispatcher;
        _eventsNames = eventsNames;
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
        if (eventName == _eventsNames.SHOOT)
        {
            _shootEvent?.Invoke();
        }
    }
}