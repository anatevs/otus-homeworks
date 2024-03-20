using UnityEngine;
using Scellecs.Morpeh;

public class HealthSystem : ISystem
{
    public World World
    {
        get => _world;
        set => _ = _world;
    }

    private Filter _filter;

    private World _world;

    public HealthSystem(World world)
    {
        _world = world;
    }

    public void OnAwake()
    {
        _filter = _world.Filter.With<HealthComponent>().Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref var hp = ref entity.GetComponent<HealthComponent>();
            Debug.Log(hp.value);
        }
    }

    public void Dispose()
    {
    }
}
