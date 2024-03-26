using Scellecs.Morpeh;
using UnityEngine;

public sealed class MovementSystem : ISystem
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
            .With<Speed>()
            .Without<Standing>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref Position position = ref entity.GetComponent<Position>();
            MoveDirection moveDirection = entity.GetComponent<MoveDirection>();
            Speed speed = entity.GetComponent<Speed>();

            //if (entity.Has<Rotation>() && (moveDirection.value != Vector3.zero))
            //{
            //    ref Rotation rotation = ref entity.GetComponent<Rotation>();
            //    rotation.value = Quaternion.LookRotation(moveDirection.value);
            //}

            position.value += moveDirection.value * speed.value * deltaTime;
        }
    }

    public void Dispose()
    {
    }
}