using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerEntity _playerEntity;
    
    [SerializeField]
    private PoolParams<Bullet> _bulletPoolParams;

    [SerializeField]
    private PoolParams<ZombieEntity> _zombieEntityPoolParams;

    [SerializeField]
    private ZombieSystem _zombieSystem;

    [SerializeField]
    private GameManager _gameManager;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntities(builder);
        RegisterPools(builder);
        RegisterZombieSystem(builder);
        RegisterUIPresenters(builder);
        RegisterGameListenersAndManager(builder);
    }

    private void RegisterEntities(IContainerBuilder builder)
    {
        builder.RegisterComponent<PlayerEntity>(_playerEntity).
            AsImplementedInterfaces();
    }

    private void RegisterPools(IContainerBuilder builder)
    {
        builder.Register<PoolManager<Bullet>>(Lifetime.Singleton).
            WithParameter(_bulletPoolParams);

        builder.Register<PoolManager<ZombieEntity>>(Lifetime.Singleton).
            WithParameter(_zombieEntityPoolParams);
    }

    private void RegisterZombieSystem(IContainerBuilder builder)
    {
        builder.RegisterComponent<ZombieSystem>(_zombieSystem);
    }

    private void RegisterUIPresenters(IContainerBuilder builder)
    {
        builder.Register<GameInfoPresenter>(Lifetime.Singleton).
            AsImplementedInterfaces().
            AsSelf();
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