using UnityEngine;

public partial class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private ParticleSystem _shootParticles;

    [SerializeField]
    private ParticleSystem _deathParticles;

    private ShootVFXMechanic _shootVFXMechanic;
    private DeathVFXMechanic _deathVFXMechanic;

    private void Awake()
    {
        _shootVFXMechanic = new ShootVFXMechanic(_player.ShootIsDone, _shootParticles);
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
