using Scellecs.Morpeh;

public class TargetDefineSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private readonly TeamService _teamService;

    public TargetDefineSystem(TeamService teamService)
    {
        _teamService = teamService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<MobFlag>()
            .With<Position>()
            .With<MoveDirection>()
            .With<Team>()
            .Build();

        foreach (Entity entity in _filter)
        {
            Entity target = SearchNearestTarget(entity);
            SetTarget(entity, target);
        }
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            Entity currTarget = SearchNearestTarget(entity);

            if (currTarget.IsNullOrDisposed())
            {
                entity.RemoveComponent<Target>();
                continue;
            }

            Entity prevTarget = entity.GetComponent<Target>().value;
            if (!prevTarget.IsNullOrDisposed() && (currTarget == prevTarget))
            {
                continue;
            }
            else
            {
                SetTarget(entity, currTarget);
            }
        }
    }

    private void SetTarget(Entity entity, Entity target)
    {
        if (entity.Has<Standing>())
        {
            entity.RemoveComponent<Standing>();
        }
        entity.SetComponent<Target>(new Target { value = target });
    }

    private Entity SearchNearestTarget(Entity entity)
    {
        return _teamService.FindNearestEnemy(
            entity.GetComponent<Position>().value,
            entity.GetComponent<Team>().value);
    }

    public void Dispose()
    {
    }
}