using Scellecs.Morpeh;
using UnityEngine;

public class TargetDefineSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Stash<Target> _targetStash;
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
            .Without<Inactive>()
            .Build();

        _targetStash = this.World.GetStash<Target>();

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
                _targetStash.Remove(entity);
                entity.RemoveComponent<AttackingTag>();

                continue;
            }

            Entity prevTarget = _targetStash.Get(entity).value;
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
        entity.RemoveComponent<StandingFlag>();
        entity.RemoveComponent<AttackingTag>();

        _targetStash.Set(entity, new Target { value = target });
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