using Scellecs.Morpeh;
using UnityEngine;

public class MovementSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    private Stash<Position> _positionStash;
    private Stash<MoveDirection> _moveDirectionStash;
    private Stash<Speed> _speedStash;
    private Stash<Rotation> _rotationStash;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<Speed>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
        _moveDirectionStash = this.World.GetStash <MoveDirection>();
        _speedStash = this.World.GetStash<Speed>();
        _rotationStash = this.World.GetStash<Rotation>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref Position position = ref _positionStash.Get(entity);
            MoveDirection moveDirection = _moveDirectionStash.Get(entity);
            Speed speed = _speedStash.Get(entity);

            if (_rotationStash.Has(entity))
            {
                ref Rotation rotation = ref _rotationStash.Get(entity);
                rotation.value = Quaternion.LookRotation(moveDirection.value);
            }

            position.value += moveDirection.value * speed.value * deltaTime;
        }
    }

    public void Dispose()
    {
    }
}