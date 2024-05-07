using Game.Gameplay.Player;
using Sample;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerStatsSystem _playerStatsSystem;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterMoneyStorage(builder);

        RegisterPlayerStats(builder);
    }

    private void RegisterMoneyStorage(IContainerBuilder builder)
    {
        builder.Register<MoneyStorage>(Lifetime.Singleton);
    }

    private void RegisterPlayerStats(IContainerBuilder builder)
    {
        builder.Register<PlayerStats>(Lifetime.Singleton);
        builder.Register<UpgradesManager>(Lifetime.Singleton);
        builder.RegisterComponent(_playerStatsSystem);
    }
}
