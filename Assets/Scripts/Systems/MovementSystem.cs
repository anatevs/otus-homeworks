using Scellecs.Morpeh;

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
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            //ref var position = ref entity.GetComponent<PositionComponent>();
            //var moveDirection = entity.GetComponent<MoveDirectionComponent>();
            //var speed = entity.GetComponent<SpeedComponent>();

            ref Position position = ref _positionStash.Get(entity);
            MoveDirection moveDirection = _moveDirectionStash.Get(entity);
            Speed speed = _speedStash.Get(entity);

            position.value += moveDirection.value * speed.value * deltaTime;
        }
    }

    public void Dispose()
    {
    }
}