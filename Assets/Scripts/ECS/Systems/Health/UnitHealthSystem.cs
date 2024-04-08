using Scellecs.Morpeh;
using System;

public sealed class UnitHealthSystem : ISystem
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
            .With<Health>()
            .Without<Inactive>()
            .With<MobFlag>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            int hp = entity.GetComponent<Health>().value;
            if (hp <= 0)
            {
                entity.AddComponent<UnspawnRequest>();
            }
        }
    }

    public void Dispose()
    {
    }
}