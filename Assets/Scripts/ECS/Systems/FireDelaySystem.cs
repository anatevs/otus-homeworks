using Scellecs.Morpeh;

public class FireDelaySystem : ISystem
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
            .With<FireDelay>()
            .With<FireDelayCounter>()
            .Without<CanFireTag>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            ref FireDelayCounter counter = ref entity.GetComponent<FireDelayCounter>();
            counter.value += deltaTime;

            float delay = entity.GetComponent<FireDelay>().value;

            if (counter.value >= delay )
            {
                entity.AddComponent<CanFireTag>();
                counter.value = 0;
            }
        }
    }

    public void Dispose()
    {
    }
}