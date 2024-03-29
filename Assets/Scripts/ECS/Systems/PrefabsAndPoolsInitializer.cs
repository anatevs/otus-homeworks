using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

public sealed class PrefabsAndPoolsInitializer : IInitializer
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private readonly PrefabsStorage _prefabStorage;

    public PrefabsAndPoolsInitializer(PrefabsStorage prefabStorage)
    {
        _prefabStorage = prefabStorage;
    }

    public void OnAwake()
    {
        PrefabParams[] prefabsInfo = _prefabStorage.GetPrefabs();
        for (int i = 0; i < prefabsInfo.Length; i++)
        {
            Entity prefabEntity = this.World.CreateEntity();
            prefabEntity.AddComponent<Team>() = prefabsInfo[i].teamType;
            prefabEntity.AddComponent<ObjectType>() = prefabsInfo[i].objectType;
            prefabEntity.AddComponent<Prefab>() = prefabsInfo[i].prefab;
            prefabEntity.AddComponent<PoolQueue>().value = new Queue<GameObject>();
        }
    }

    public void Dispose()
    {
    }
}