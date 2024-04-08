using Scellecs.Morpeh;
using System;

public sealed class TakeDamageSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _changeFilter;

    public void OnAwake()
    {
        _changeFilter = this.World.Filter
            .With<TakeDamageEvent>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _changeFilter)
        {
            ref Health health = ref entity.GetComponent<Health>();

            health.value -= entity.GetComponent<TakeDamageEvent>().value;
            health.value = Math.Max(0, health.value);
        }
    }

    public void Dispose()
    {
    }
}