using Scellecs.Morpeh;

public sealed class ChangeHealthSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _changeFilter;

    public void OnAwake()
    {
        _changeFilter = this.World.Filter
            .With<TakeDamageEvent>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _changeFilter)
        {
            entity.GetComponent<Health>().value -=
                entity.GetComponent<TakeDamageEvent>().value;
        }
    }

    public void Dispose()
    {
    }
}