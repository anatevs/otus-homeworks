using Scellecs.Morpeh;
using UnityEngine;

public class AttackDistanceSystem : ISystem
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
            .With<Position>()
            .With<AttackDistance>()
            .With<Target>()
            .Without<Standing>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            float attackDistance = entity.GetComponent<AttackDistance>().value;
            Vector3 position = entity.GetComponent<Position>().value;
            Vector3 targetPosition = entity.GetComponent<Target>().value.GetComponent<Position>().value;
            float sqrDistance = Vector3.SqrMagnitude(position - targetPosition);
            if ((sqrDistance <= attackDistance * attackDistance) && entity.Has<CanFireTag>())
            {
                entity.AddComponent<FireRequest>();
            }
            else
            {
                entity.RemoveComponent<Standing>();
            }
        }
    }

    public void Dispose()
    {
        
    }
}