using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using VContainer;

public class FireRequestSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    [Inject]
    private World _eventsWorld;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<FireRequest>()
            .With<Target>()
            .Build();

        
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            entity.AddComponent<Standing>();

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {

    }
}