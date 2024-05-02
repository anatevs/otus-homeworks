using GameEngine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameSceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UnitManager _unitManager;

    [SerializeField]
    private ResourceService _resourceService;

    [SerializeField]
    private GameListenersManager _gameListenersManager;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterObjectsServices(builder);

        RegisterDefaultSceneObjects(builder);

        RegisterGameListeners(builder);
    }

    private void RegisterObjectsServices(IContainerBuilder builder)
    {
        builder.RegisterComponent<UnitManager>(_unitManager);
        builder.RegisterComponent<ResourceService>(_resourceService);
    }

    private void RegisterDefaultSceneObjects(IContainerBuilder builder)
    {
        builder.Register<SceneObjectsService>(Lifetime.Singleton);
    }

    private void RegisterGameListeners(IContainerBuilder builder)
    {
        builder.RegisterComponent<GameListenersManager>(_gameListenersManager);
        builder.Register<SaveLoadManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

        builder.Register<GameListenersInstaller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}