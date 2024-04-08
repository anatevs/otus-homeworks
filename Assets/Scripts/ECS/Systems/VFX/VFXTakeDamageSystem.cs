using Scellecs.Morpeh;
using UnityEngine;

public class VFXTakeDamageSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<TakeDamageEvent>()
            .With<DamageVFX>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            ParticleSystem particleSystem = entity.GetComponent<DamageVFX>().value;
            particleSystem.Play();
        }
    }

    public void Dispose()
    {
    }
}