using Scellecs.Morpeh;
using UnityEngine;

public sealed class PrefabsInitializer : IInitializer
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private readonly PrefabsStorage _prefabStorage;

    public PrefabsInitializer(PrefabsStorage prefabStorage)
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
            prefabEntity.AddComponent<PrefabType>() = prefabsInfo[i].prefabType;
            prefabEntity.AddComponent<Prefab>() = prefabsInfo[i].prefab;
        }
    }

    public void Dispose()
    {
        
    }

}