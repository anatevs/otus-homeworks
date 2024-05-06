using Game.Gameplay.Player;
using Sample;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private PlayerStatsSystem _playerStatsSystem;

    [SerializeField]
    private TestUpgrade _testUpgrade;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterMoneyStorage(builder);

        RegisterPlayerStats(builder);

        RegisterWoodConverter(builder);
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

    private void RegisterWoodConverter(IContainerBuilder builder)
    {
        builder.Register<WoodConverter>(Lifetime.Singleton);
        builder.RegisterComponent(_testUpgrade);
    }
}
