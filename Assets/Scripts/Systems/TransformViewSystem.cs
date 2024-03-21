using Scellecs.Morpeh;

public class TransformViewSystem : ILateSystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    private Stash<Position> _positionStash;
    private Stash<TransformView> _transformView;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<Speed>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
        _transformView = this.World.GetStash<TransformView>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Position position = _positionStash.Get(entity);
            ref TransformView transformView = ref _transformView.Get(entity);

            transformView.value.position = position.value;
        }
    }

    public void Dispose()
    {
    }
}