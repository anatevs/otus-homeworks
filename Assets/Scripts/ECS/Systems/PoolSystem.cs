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
    private Filter _prefabsFilter;

    private Stash<Team> _teamStash;
    private Stash<ObjectType> _typeStash;
    private Stash<Inactive> _poolStash; 

    private ObjectsTypeNames _arrow;

    public void OnAwake()
    {
        _spawnFilter = this.World.Filter
            .With<SpawnRequest>()
            .Build();

        _prefabsFilter = this.World.Filter
            .With<Prefab>()
            .With<ObjectType>()
            .With<Team>()
            .Build();

        _prefabsFilter = this.World.Filter
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
            

            //Instantiate
            foreach (Entity prefabEntity in _prefabsFilter)
            {
                if (spawnRequest.teamType == _teamStash.Get(prefabEntity).value &&
                    spawnRequest.typeName == _typeStash.Get(prefabEntity).value)
                {
                    GameObject newGameObject = GameObject.Instantiate(
                        prefabEntity.GetComponent<Prefab>().prefab,
                        spawnRequest.position,
                        spawnRequest.rotation);

                    Entity newEntity = newGameObject.GetComponent<MovableProvider>().Entity;
                    if (newEntity.Has<Inactive>())
                    {
                        newEntity.RemoveComponent<Inactive>();
                    }
                    break;
                }
            }

            spawnEntity.RemoveComponent<FireRequest>();
        }
    }
    public void Dispose()
    {
        
    }

}