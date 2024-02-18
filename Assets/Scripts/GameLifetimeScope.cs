using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using GameEngine;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UnitManager _unitManager;

    [SerializeField]
    private ResourceService _resourceService;

    [SerializeField]
    private UnitPrefabsList _unitPrefabs;

    [SerializeField]
    private ScriptableObject[] defaultConfigs;

    [SerializeField]
    private GameListenersManager _gameListenersManager;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterObjectsServices(builder);

        RegisterPrefabsLists(builder);

        RegisterDefaultSceneObjects(builder);

        RegisterDefaultObjectsParams(builder);

        RegisterGameRepository(builder);

        RegisterSaveLoaders(builder);

        RegisterGameListeners(builder);
    }

    private void RegisterObjectsServices(IContainerBuilder builder)
    {
        builder.RegisterComponent<UnitManager>(_unitManager);
        builder.RegisterComponent<ResourceService>(_resourceService);
    }

    private void RegisterPrefabsLists(IContainerBuilder builder)
    {
        builder.Register<UnitPrefabsManager>(Lifetime.Singleton).
            WithParameter<UnitPrefabsList>(_unitPrefabs);
    }

    private void RegisterDefaultSceneObjects(IContainerBuilder builder)
    {
        builder.Register<OnSceneObjectsService>(Lifetime.Singleton);
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
        builder.Register<SaveLoadUnits>(Lifetime.Singleton);
        builder.Register<SaveLoadResources>(Lifetime.Singleton);
    }

    private void RegisterGameListeners(IContainerBuilder builder)
    {
        builder.RegisterComponent<GameListenersManager>(_gameListenersManager);
        builder.Register<SaveLoadManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

        builder.Register<GameListenersInstaller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}