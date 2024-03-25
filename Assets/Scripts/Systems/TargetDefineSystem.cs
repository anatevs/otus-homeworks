using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class TargetDefineSystem<TPlayer, TEnemy> : ISystem
    where TPlayer : ITeam
    where TEnemy : ITeam
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    private readonly TeamService<TEnemy> _enemiesService;
    private readonly TPlayer _playerTeam;

    public TargetDefineSystem(TPlayer playerTeam,TeamService<TEnemy> enemiesService)
    {
        _playerTeam = playerTeam;
        _enemiesService = enemiesService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .Build();

        foreach (Entity entity in _filter)
        {
            if (entity.GetComponent<Team>().value == _playerTeam.TeamType)
            {
                Entity target = SearchNearestTarget(entity);
                SetTarget(entity, target);
            }
        }

    }

    public void OnUpdate(float deltaTime)
    {
        foreach(Entity entity in _filter)
        {
            if (entity.GetComponent<Team>().value == _playerTeam.TeamType)
            {
                Entity currTarget = SearchNearestTarget(entity);
                Entity prevTarget = entity.GetComponent<Target>().value;
                if (prevTarget.Has<IsActive>() && (currTarget == prevTarget))
                {
                    continue;
                }
                else
                {
                    SetTarget(entity, currTarget);
                }
            }
            else
            {
                continue;
            }
        }
    }

    private void SetTarget(Entity entity, Entity target)
    {
        Entity enemy = SearchNearestTarget(entity);
        enemy.AddComponent<UnderAttackTag>();
        entity.SetComponent<Target>(new Target { value = enemy });
    }

    private Entity SearchNearestTarget(Entity entity)
    {
        return _enemiesService.FindNearest(entity.GetComponent<Position>().value);
    }

    public void Dispose()
    {
    }
}