using GameEngine;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UnitPrefabsList _unitPrefabs;

    [SerializeField]
    private ScriptableObject[] defaultConfigs;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterPrefabsLists(builder);

        RegisterDefaultObjectsParams(builder);

        RegisterGameRepository(builder);

        RegisterSaveLoaders(builder);
    }

    private void RegisterPrefabsLists(IContainerBuilder builder)
    {
        builder.Register<UnitPrefabsManager>(Lifetime.Singleton).
            WithParameter<UnitPrefabsList>(_unitPrefabs);
    }

    private void RegisterDefaultObjectsParams(IContainerBuilder builder)
    {
        Dictionary<string, ScriptableObject> defaultParams = new Dictionary<string, ScriptableObject>();
        for (int i = 0; i < defaultConfigs.Length; i++)
        {
            defaultParams[defaultConfigs[i].GetType().Name] = defaultConfigs[i];
        }
        builder.RegisterComponent<Dictionary<string, ScriptableObject>>(defaultParams);
    }

    private void RegisterGameRepository(IContainerBuilder builder)
    {
        builder.Register<GameRepository>(Lifetime.Singleton);
    }

    private void RegisterSaveLoaders(IContainerBuilder builder)
    {
        builder.Register<SaveLoadUnits>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<SaveLoadResources>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}
