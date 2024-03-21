using Scellecs.Morpeh;

public class DirectToTargetSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Stash<Position> _positionStash;
    private Stash<MoveDirection> _moveDirectionStash;
    private Stash<TargetPosition> _targetPositionStash;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<TargetPosition>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
        _moveDirectionStash = this.World.GetStash<MoveDirection>();
        _targetPositionStash = this.World.GetStash<TargetPosition>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Position position = _positionStash.Get(entity);
            ref MoveDirection moveDirection = ref _moveDirectionStash.Get(entity);
            TargetPosition targetPosition = _targetPositionStash.Get(entity);

            moveDirection.value = (targetPosition.value - position.value).normalized;
        }
    }

    public void Dispose()
    {
    }
}