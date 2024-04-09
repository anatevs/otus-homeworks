using Scellecs.Morpeh;

public sealed class FireReloadingSystem : ISystem
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
            .With<MobFlag>()
            .With<DelayCounter>()
            .Without<CanFireTag>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            ref DelayCounter counter = ref entity.GetComponent<DelayCounter>();
            counter.counter += deltaTime;

            float delay = entity.GetComponent<DelayCounter>().delay;

            if (counter.counter >= delay )
            {
                entity.AddComponent<CanFireTag>();
                counter.counter = 0;
            }
        }
    }

    public void Dispose()
    {
    }
}