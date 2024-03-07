using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVFX : MonoBehaviour
{
    [SerializeField]
    private Zombie _zombie;

    [SerializeField]
    private ParticleSystem _onDamageParticles;

    private OnEventVFXMechanic<int> _onDamageVFXMechanic;

    private void Awake()
    {
        _onDamageVFXMechanic = new OnEventVFXMechanic<int>(_zombie.OnDamage, _onDamageParticles);
    }

    private void OnEnable()
    {
        _onDamageVFXMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _onDamageVFXMechanic.OnDisable();
    }
}
