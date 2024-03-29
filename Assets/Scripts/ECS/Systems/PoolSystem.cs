using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _spawnFilter;
    private Filter _prefabsAndPoolFilter;
    private Filter _inPoolFilter;

    private Stash<Team> _teamStash;
    private Stash<ObjectType> _typeStash;

    private ObjectsTypeNames _arrow;

    public void OnAwake()
    {
        _spawnFilter = this.World.Filter
            .With<SpawnRequest>()
            .Build();

        _prefabsAndPoolFilter = this.World.Filter
            .With<Prefab>()
            .With<PoolParams>()
            .With<ObjectType>()
            .With<Team>()
            .Build();

        _inPoolFilter = this.World.Filter
            .With<ObjectType>()
            .With<Team>()
            .With<Inactive>()
            .Build();

        _arrow = ObjectsTypeNames.Arrow;
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity spawnEntity in _spawnFilter)
        {
            SpawnRequest spawnRequest = spawnEntity.GetComponent<SpawnRequest>();
            PoolParams poolParams = new();
            GameObject spawnGO = null;
            foreach (Entity prefabEntity in _prefabsAndPoolFilter)
            {
                if (spawnRequest.teamType == _teamStash.Get(prefabEntity).value &&
                    spawnRequest.typeName == _typeStash.Get(prefabEntity).value)
                {
                    poolParams = prefabEntity.GetComponent<PoolParams>();

                    if (TryGetFromPool(spawnRequest, out spawnGO))
                    {
                        break;
                    }
                    else
                    {
                        spawnGO = SpawnNew(prefabEntity, spawnRequest);
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }

            if (spawnGO == null)
            {
                throw new System.Exception($"no valid prefab for params: {spawnRequest.teamType} and {spawnRequest.typeName}");
            }

            spawnGO.transform.SetParent(poolParams.worldTransform);
            spawnGO.SetActive(true);
            spawnEntity.RemoveComponent<FireRequest>();
        }
    }

    private bool TryGetFromPool(SpawnRequest spawnRequest, out GameObject res)
    {
        foreach (Entity poolEntity in _inPoolFilter)
        {
            if (spawnRequest.teamType == poolEntity.GetComponent<Team>().value &&
                spawnRequest.typeName == poolEntity.GetComponent<ObjectType>().value)
            {
                poolEntity.GetComponent<Position>().value = spawnRequest.spawnTransform.position;
                poolEntity.GetComponent<Rotation>().value = spawnRequest.spawnTransform.rotation;
                poolEntity.GetComponent<MoveDirection>().value = spawnRequest.spawnTransform.forward;
                poolEntity.RemoveComponent<Inactive>();

                res = poolEntity.GetComponent<TransformView>().value.gameObject;
                return true;
            }
        }
        res = null;
        return false;
    }

    private GameObject SpawnNew(Entity prefabEntity, SpawnRequest spawnRequest)
    {
        GameObject newGameObject = GameObject.Instantiate(
                            prefabEntity.GetComponent<Prefab>().prefab,
                            spawnRequest.spawnTransform.position,
                            spawnRequest.spawnTransform.rotation);

        Entity newEntity = newGameObject.GetComponent<MovableProvider>().Entity;
        newEntity.RemoveComponent<Inactive>();

        return newGameObject;
    }

    public void Dispose()
    {
        
    }

}