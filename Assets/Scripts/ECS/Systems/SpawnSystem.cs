using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _requestFilter;
    private Filter _prefabsAndPoolFilter;

    private Stash<Team> _teamStash;
    private Stash<ObjectType> _typeStash;

    public void OnAwake()
    {
        _requestFilter = this.World.Filter
            .With<SpawnRequest>()
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
        foreach (Entity entity in _requestFilter)
        {
            SpawnRequest spawnRequest = entity.GetComponent<SpawnRequest>();
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

            ActivateSpawned(spawnGO, poolParams.worldTransform);

            entity.RemoveComponent<SpawnRequest>();
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

    private GameObject SpawnNew(Entity prefabEntity, SpawnRequest spawnRequest)
    {
        GameObject newGO = GameObject.Instantiate(
                            prefabEntity.GetComponent<Prefab>().prefab,
                            spawnRequest.transform.position,
                            spawnRequest.transform.rotation);

        return newGO;
    }

    private void ActivateSpawned(GameObject go, Transform worldTransform)
    {
        go.transform.SetParent(worldTransform);
        go.SetActive(true);

        Entity spawnEntity = go.GetComponent<MovableProvider>().Entity;
        spawnEntity.RemoveComponent<Inactive>();
    }

    public void Dispose()
    {
    }

}