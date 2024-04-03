using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelaySystem : ISystem
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
            .With<MobFlag>()
            .With<AttackDelay>()
            .With<AttackCounter>()
            .Without<CanFireTag>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            ref AttackCounter counter = ref entity.GetComponent<AttackCounter>();
            counter.value += deltaTime;

            float delay = entity.GetComponent<AttackDelay>().value;

            if (counter.value >= delay )
            {
                entity.AddComponent<CanFireTag>();
                counter.value = 0;
            }
        }
    }

    public void Dispose()
    {
    }
}