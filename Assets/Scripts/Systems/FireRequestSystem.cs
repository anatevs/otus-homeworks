using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRequestSystem : ISystem
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
            .With<FireRequest>()
            .With<Target>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            entity.GetComponent<MoveDirection>().value = Vector3.zero;

            Debug.Log($"fire request from {entity}");

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {

    }
}
