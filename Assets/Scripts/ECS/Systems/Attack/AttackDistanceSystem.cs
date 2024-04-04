using Scellecs.Morpeh;
using UnityEngine;

public sealed class AttackDistanceSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Stash<Position> _positionStash;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<AttackDistance>()
            .With<Target>()
            .Without<Inactive>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            float attackDistance = entity.GetComponent<AttackDistance>().value;
            Vector3 position = _positionStash.Get(entity).value;
            
            Entity target = entity.GetComponent<Target>().value;
            Vector3 targetPosition = _positionStash.Get(target).value;
            float targetStop = target.GetComponent<StoppingDistance>().value;
            float sqrDistance = Vector3.SqrMagnitude(position - targetPosition);
            float stopDistance = attackDistance + targetStop;
            if ((sqrDistance <= stopDistance * stopDistance))
            {
                entity.SetComponent(new StandingFlag());

                if (!entity.Has<AttackingTag>())
                {
                    entity.AddComponent<FireRequest>();
                }
            }
        }
    }

    public void Dispose()
    {
        
    }
}