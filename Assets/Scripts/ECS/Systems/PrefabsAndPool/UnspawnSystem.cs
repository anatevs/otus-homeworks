using Scellecs.Morpeh;
using UnityEngine;

public sealed class UnspawnSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _unspawnFilter;
    private Filter _prefabsAndPoolFilter;

    private Stash<Team> _teamStash;
    private Stash<ObjectType> _typeStash;

    private TeamService _teamService;

    public UnspawnSystem(TeamService teamService)
    {
        _teamService = teamService;
    }

    public void OnAwake()
    {
        _unspawnFilter = this.World.Filter
            .With<UnspawnRequest>()
            .Build();

        _prefabsAndPoolFilter = this.World.Filter
            .With<Prefab>()
            .With<PoolParams>()
            .With<ObjectType>()
            .With<Team>()
            .Build();

        _teamStash = this.World.GetStash<Team>();
        _typeStash = this.World.GetStash<ObjectType>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _unspawnFilter)
        {
            bool prefabExist = false;
            foreach (Entity prefabEntity in _prefabsAndPoolFilter)
            {
                if (_teamStash.Get(entity).value == _teamStash.Get(prefabEntity).value &&
                    _typeStash.Get(entity).value == _typeStash.Get(prefabEntity).value)
                {
                    PoolParams poolParams = prefabEntity.GetComponent<PoolParams>();
                    Unspawn(entity, poolParams);
                    prefabExist = true;
                    break;
                }
            }

            if (!prefabExist)
            {
                throw new System.Exception(
                    $"no valid prefab for unspawn object with params: " +
                    $"{_teamStash.Get(entity).value} and {_typeStash.Get(entity).value}");
            }

            entity.RemoveComponent<UnspawnRequest>();
        }
    }

    private void Unspawn(Entity entity, PoolParams poolParams)
    {
        entity.SetComponent<Inactive>(new Inactive());
        _teamService.RemoveFromTeam(entity);

        GameObject go = entity.GetComponent<TransformView>().value.gameObject;
        go.SetActive(false);
        go.transform.SetParent(poolParams.poolTransform);
        poolParams.pool.Enqueue(go);
    }

    public void Dispose()
    {
    }
}