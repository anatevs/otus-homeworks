using UnityEngine;

public class OnEventVFXMechanic
{
    private readonly IAtomicEvent _event;
    private readonly ParticleSystem _onEventVFX;

    public OnEventVFXMechanic(IAtomicEvent eventForVFX, ParticleSystem onEventVFX)
    {
        _event = eventForVFX;
        _onEventVFX = onEventVFX;
    }

    public void OnEnable()
    {
        _event.Subscribe(PlayVFX);
    }

    public void OnDisable()
    {
        _event.Unsubscribe(PlayVFX);
    }

    private void PlayVFX()
    {
        _onEventVFX.Play();
    }
}

public class OnEventVFXMechanic<T>
{
    private readonly IAtomicEvent<T> _event;
    private readonly ParticleSystem _onEventVFX;

    public OnEventVFXMechanic(IAtomicEvent<T> @event, ParticleSystem onEventVFX)
    {
        _event = @event;
        _onEventVFX = onEventVFX;
    }

    public void OnEnable()
    {
        _event.Subscribe(PlayVFX);
    }

    public void OnDisable()
    {
        _event.Unsubscribe(PlayVFX);
    }

    private void PlayVFX(T _)
    {
        _onEventVFX.Play();
    }
}