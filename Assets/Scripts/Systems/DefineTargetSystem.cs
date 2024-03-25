using Scellecs.Morpeh;
using UnityEngine;

public class DefineTargetSystem<T> : ISystem where T : ITeam
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private TeamService<T> _enemiesService;

    public DefineTargetSystem(TeamService<T> enemiesService)
    {
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
            if (entity.GetComponent<Team>().value == TeamType.Blue)
            {
                DefineTarget(entity);
            }
        }

    }

    public void OnUpdate(float deltaTime)
    {
        foreach(Entity entity in _filter)
        {
            if (entity.GetComponent<Team>().value == TeamType.Blue)
            {
                Entity prevTarget = entity.GetComponent<Target>().value;
                if (prevTarget.Has<IsActive>())
                {
                    continue;
                }
                else
                {
                    DefineTarget(entity);
                }
            }
            else
            {
                continue;
            }
        }
    }


    private void DefineTarget(Entity entity)
    {
        Entity enemy = _enemiesService.FindNearest(entity.GetComponent<Position>().value);
        if (enemy == null)
        {
            Debug.Log("no enemies i.e. game over!!");
        }
        else
        {
            enemy.AddComponent<UnderAttackTag>();
            entity.SetComponent<Target>(new Target { value = enemy });
            Debug.Log($"{entity.ID} has enemy {enemy.ID}");
        }
    }

    public void Dispose()
    {
    }
}
