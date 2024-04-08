using Scellecs.Morpeh;

public class BaseHealthSystem : ISystem
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
            .With<Health>()
            .Without<Inactive>()
            .With<BaseFlag>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            int hp = entity.GetComponent<Health>().value;
            if (hp <= 0)
            {
                if (!entity.GetComponent<DestroyVFX>().value.isPlaying)
                {
                    entity.GetComponent<DestroyVFX>().value.Play();
                }
            }
        }
    }

    public void Dispose()
    {
    }
}