using UnityEngine;

public class DeathVFXMechanic
{
    private readonly AtomicVariable<bool> _isDead;
    private readonly ParticleSystem _deathVFX;

    public DeathVFXMechanic(AtomicVariable<bool> isDead, ParticleSystem shootVFX)
    {
        _isDead = isDead;
        _deathVFX = shootVFX;
    }

    public void OnEnable()
    {
        _isDead.Subscribe(PlayVFX);
    }

    public void OnDisable()
    {
        _isDead.Unsubscribe(PlayVFX);
    }

    private void PlayVFX(bool isDead)
    {
        if (isDead)
        {
            _deathVFX.Play();
        }
    }
}