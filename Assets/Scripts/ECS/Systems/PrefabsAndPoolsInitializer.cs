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
        PrefabAndPoolParams[] info = _prefabStorage.GetPrefabs();
        for (int i = 0; i < info.Length; i++)
        {
            Entity infoEntity = this.World.CreateEntity();
            infoEntity.AddComponent<Team>() = info[i].teamType;
            infoEntity.AddComponent<ObjectType>() = info[i].objectType;
            infoEntity.AddComponent<Prefab>() = info[i].prefab;
            infoEntity.AddComponent<PoolParams>() = new PoolParams()
            {
                pool = new Queue<GameObject>(),
                poolTransform = info[i].poolTransform,
                worldTransform = info[i].worldTransform
            };

            for (int j = 0; j < info[i].initPoolCount; j++)
            {
                GameObject go = GameObject.Instantiate(info[i].prefab.prefab);
                go.SetActive(false);
                go.transform.SetParent(info[i].poolTransform);
                infoEntity.GetComponent<PoolParams>().pool.Enqueue(go);
            }
        }
    }

    public void Dispose()
    {
    }
}