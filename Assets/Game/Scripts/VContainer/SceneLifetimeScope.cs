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
    private PoolParams<Zombie> _zombiePoolParams;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterEntities(builder);
        RegisterUIPresenters(builder);
        RegisterPools(builder);
    }


    private void RegisterEntities(IContainerBuilder builder)
    {
        builder.RegisterComponent<PlayerEntity>(_playerEntity);
    }

    private void RegisterUIPresenters(IContainerBuilder builder)
    {
        builder.Register<GameInfoPresenter>(Lifetime.Singleton).
            AsImplementedInterfaces().
            AsSelf();
    }

    private void RegisterPools(IContainerBuilder builder)
    {
        builder.Register<PoolManager<Bullet>>(Lifetime.Singleton).
            WithParameter(_bulletPoolParams);

        builder.Register<PoolManager<Zombie>>(Lifetime.Singleton).
            WithParameter(_zombiePoolParams);
    }
}