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

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntities(builder);
        RegisterPools(builder);
        RegisterZombieSystem(builder);
        RegisterUIPresenters(builder);
    }


    private void RegisterEntities(IContainerBuilder builder)
    {
        builder.RegisterComponent<PlayerEntity>(_playerEntity);
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
}