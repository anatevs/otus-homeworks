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
    private Stash<Target> _targetStash;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<Target>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
        _moveDirectionStash = this.World.GetStash<MoveDirection>();
        _targetStash = this.World.GetStash<Target>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Position position = _positionStash.Get(entity);
            ref MoveDirection moveDirection = ref _moveDirectionStash.Get(entity);
            Position target = _positionStash.Get(_targetStash.Get(entity).value);

            moveDirection.value = (target.value - position.value).normalized;
        }
    }

    public void Dispose()
    {
    }
}