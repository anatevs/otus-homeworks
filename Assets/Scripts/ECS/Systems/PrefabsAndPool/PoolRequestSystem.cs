using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

public class PoolRequestSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _spawnFilter;
    private Filter _prefabsAndPoolFilter;

    private Stash<Team> _teamStash;
    private Stash<ObjectType> _typeStash;

    public void OnAwake()
    {
        _spawnFilter = this.World.Filter
            .With<PoolRequest>()
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
        foreach (Entity entity in _spawnFilter)
        {
            PoolRequest spawnRequest = entity.GetComponent<PoolRequest>();
            PoolParams poolParams = new();
            GameObject spawnGO = null;
            foreach (Entity prefabEntity in _prefabsAndPoolFilter)
            {
                if (spawnRequest.team == _teamStash.Get(prefabEntity).value &&
                    spawnRequest.type == _typeStash.Get(prefabEntity).value)
                {
                    poolParams = prefabEntity.GetComponent<PoolParams>();

                    if (TryGetFromPool(spawnRequest.transform, poolParams.pool, out spawnGO))
                    {
                        break;
                    }
                    else
                    {
                        spawnGO = SpawnNew(prefabEntity, spawnRequest);
                        break;
                    }
                }
            }

            if (spawnGO == null)
            {
                throw new System.Exception(
                    $"no valid prefab for params: " +
                    $"{spawnRequest.team} and {spawnRequest.type}");
            }

            entity.SetComponent(new SpawnRequest() { spawnGO = spawnGO, worldTransform = poolParams.worldTransform });
            entity.RemoveComponent<PoolRequest>();
        }
    }

    private bool TryGetFromPool(Transform transform, Queue<GameObject> pool, out GameObject go)
    {
        if (pool.TryDequeue(out go))
        {
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            return true;

        }
        go = null;
        return false;
    }

    private GameObject SpawnNew(Entity prefabEntity, PoolRequest spawnRequest)
    {
        GameObject newGO = GameObject.Instantiate(
                            prefabEntity.GetComponent<Prefab>().prefab,
                            spawnRequest.transform.position,
                            spawnRequest.transform.rotation);

        return newGO;
    }

    public void Dispose()
    {
    }
}