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
        PrefabAndPoolParams[] prefabsAndPoolInfo = _prefabStorage.GetPrefabs();
        for (int i = 0; i < prefabsAndPoolInfo.Length; i++)
        {
            Entity prefabAndPoolEntity = this.World.CreateEntity();
            prefabAndPoolEntity.AddComponent<Team>() = prefabsAndPoolInfo[i].teamType;
            prefabAndPoolEntity.AddComponent<ObjectType>() = prefabsAndPoolInfo[i].objectType;
            prefabAndPoolEntity.AddComponent<Prefab>() = prefabsAndPoolInfo[i].prefab;
            prefabAndPoolEntity.AddComponent<PoolParams>() = new PoolParams()
            {
                queue = new Queue<GameObject>(),
                poolTransform = prefabsAndPoolInfo[i].poolTransform,
                worldTransform = prefabsAndPoolInfo[i].worldTransform
            };
        }
    }

    public void Dispose()
    {
    }
}