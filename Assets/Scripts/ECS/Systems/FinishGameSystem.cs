using Scellecs.Morpeh;

public sealed class FinishGameSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _basesFilter;
    private Filter _gameObjectsFilter;
    private Stash<FinishGameRequest> _finishStash;

    private readonly FinishGameWindow _finishWindow;

    public FinishGameSystem(FinishGameWindow finishWindow)
    {
        _finishWindow = finishWindow;
    }

    public void OnAwake()
    {
        _basesFilter = this.World.Filter
            .With<BaseFlag>()
            .Without<Inactive>()
            .Build();

        _gameObjectsFilter = this.World.Filter
            .With<Team>()
            .Build();

        _finishStash = this.World.GetStash<FinishGameRequest>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _basesFilter)
        {
            if (_finishStash.Has(entity))
            {
                foreach (Entity gameEntity in _gameObjectsFilter)
                {
                    gameEntity.SetComponent<Inactive>(new Inactive());
                }

                TeamType winner = (TeamType)(((int)entity.GetComponent<Team>().value + 1) % 2);
                _finishWindow.Show(winner);
            }
        }
    }

    public void Dispose()
    {
    }
}