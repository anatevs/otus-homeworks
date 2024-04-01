using Scellecs.Morpeh;

public class SpawnProjectileSystem : ISystem
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
            .With<SpawnProjectileRequest>()
            .With<Weapon>()
            .With<Team>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            Weapon weapon = entity.GetComponent<Weapon>();

            entity.AddComponent<SpawnRequest>() = new SpawnRequest()
            {
                type = weapon.projectileType,
                team = entity.GetComponent<Team>().value,
                transform = weapon.firePoint
            };

            entity.RemoveComponent<SpawnProjectileRequest>();
        }
    }

    public void Dispose()
    {
    }
}