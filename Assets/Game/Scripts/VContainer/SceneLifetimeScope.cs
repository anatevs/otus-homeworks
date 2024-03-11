using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerEntity _playerEntity;
    
    [SerializeField]
    private PoolParams<Bullet> _bulletParams;
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
            WithParameter(_bulletParams);
    }
}