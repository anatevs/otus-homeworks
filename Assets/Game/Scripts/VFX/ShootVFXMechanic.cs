using UnityEngine;

public class ShootVFXMechanic
{
    private readonly IAtomicEvent _onShoot;
    private readonly ParticleSystem _shootVFX;

    public ShootVFXMechanic(IAtomicEvent onShoot, ParticleSystem shootVFX)
    {
        _onShoot = onShoot;
        _shootVFX = shootVFX;
    }

    public void OnEnable()
    {
        _onShoot.Subscribe(PlayVFX);
    }

    public void OnDisable()
    {
        _onShoot.Unsubscribe(PlayVFX);
    }

    private void PlayVFX()
    {
        _shootVFX.Play();
    }
}