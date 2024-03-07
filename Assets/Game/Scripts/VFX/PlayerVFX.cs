using UnityEngine;

public partial class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private ParticleSystem _shootParticles;

    [SerializeField]
    private ParticleSystem _deathParticles;

    private OnEventVFXMechanic _shootVFXMechanic;
    private DeathVFXMechanic _deathVFXMechanic;

    private void Awake()
    {
        _shootVFXMechanic = new OnEventVFXMechanic(_player.ShootIsDone, _shootParticles);
        _deathVFXMechanic = new DeathVFXMechanic(_player.isDead, _deathParticles);
    }

    private void OnEnable()
    {
        _shootVFXMechanic.OnEnable();
        _deathVFXMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _shootVFXMechanic.OnDisable();
        _deathVFXMechanic.OnDisable();
    }
}
