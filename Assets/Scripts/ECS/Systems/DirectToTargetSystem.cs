using Scellecs.Morpeh;
using UnityEngine;

public class DirectToTargetSystem : ISystem
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
            .With<MoveDirection>()
            .With<Target>()
            .Without<Inactive>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Position position = entity.GetComponent<Position>();
            ref MoveDirection moveDirection = ref entity.GetComponent<MoveDirection>();
            Position target = entity.GetComponent<Target>().value.GetComponent<Position>();
            Vector3 direction = (target.value - position.value).normalized;
            direction.y = 0;

            moveDirection.value = direction;
        }
    }

    public void Dispose()
    {
    }
}