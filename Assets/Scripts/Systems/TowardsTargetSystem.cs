using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using VContainer;

public class TowardsTargetSystem<T> : ISystem where T : ITeam
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Stash<Target> _targetStash;
    private Stash<UnderAttackTag> _underAttackStash;

    private TeamService<T> _enemiesService;

    public TowardsTargetSystem(TeamService<T> enemiesService)
    {
        _enemiesService = enemiesService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach(Entity entity in _filter)
        {
            Entity enemy = _enemiesService.FindNearest(entity.GetComponent<Position>().value);
            if (enemy == null)
            {
                Debug.Log("no enemies i.e. game over!!");
            }
            else
            {
                if (!entity.Has<Target>())
                {
                    entity.AddComponent<Target>();
                }
                else
                {
                    Entity prevTarget = _targetStash.Get(entity).value;
                    if (prevTarget != enemy)
                    {
                        prevTarget.RemoveComponent<UnderAttackTag>();
                    }
                }
                enemy.AddComponent<UnderAttackTag>();
                entity.SetComponent<Target>(new Target { value = enemy });
                ref MoveDirection moveDirection = ref entity.GetComponent<MoveDirection>();
                moveDirection.value = (enemy.GetComponent<Position>().value - entity.GetComponent<Position>().value).normalized;
            }
        }
    }

    public void Dispose()
    {
    }
}
