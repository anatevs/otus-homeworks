using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    [SerializeField]
    private PoolParams<ZombieEntity> _zombieEntityPoolParams;

    [SerializeField]
    private ZombieSystem _zombieSystem;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private GameInfoView _gameInfoView;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntities(builder);
        RegisterPools(builder);
        RegisterZombieSystem(builder);
        RegisterUI(builder);
        RegisterGameListenersAndManager(builder);
    }

    private void RegisterEntities(IContainerBuilder builder)
    {
        builder.RegisterComponent<PlayerEntity>(_playerEntity).
            AsImplementedInterfaces();
    }

    private void RegisterPools(IContainerBuilder builder)
    {
        builder.Register<PoolManager<ZombieEntity>>(Lifetime.Singleton).
            WithParameter(_zombieEntityPoolParams);
    }

    private void RegisterZombieSystem(IContainerBuilder builder)
    {
        builder.RegisterComponent<ZombieSystem>(_zombieSystem);
    }

    private void RegisterUI(IContainerBuilder builder)
    {
        builder.Register<GameInfoModel>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<GameInfoController>(Lifetime.Singleton)
            .WithParameter<GameInfoView>(_gameInfoView)
            .AsImplementedInterfaces()
            .AsSelf();
    }

    private void RegisterGameListenersAndManager(IContainerBuilder builder)
    {
        builder.Register<GameListenersContainer>(Lifetime.Singleton);
        builder.Register<GameListenersInstaller>(Lifetime.Singleton).
            AsImplementedInterfaces();
        builder.RegisterComponent<GameManager>(_gameManager);
        builder.Register<FinishGameManager>(Lifetime.Singleton).
            AsImplementedInterfaces();
    }
}