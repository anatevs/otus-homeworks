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
    private Stash<Rotation> _rotationStash;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<Speed>()
            .Build();

        _positionStash = this.World.GetStash<Position>();
        _transformView = this.World.GetStash<TransformView>();
        _rotationStash = this.World.GetStash<Rotation>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Position position = _positionStash.Get(entity);
            ref TransformView transformView = ref _transformView.Get(entity);

            if (_rotationStash.Has(entity))
            {
                Rotation rotation = _rotationStash.Get(entity);
                transformView.value.rotation = rotation.value;
            }

            transformView.value.position = position.value;
        }
    }

    public void Dispose()
    {
    }
}