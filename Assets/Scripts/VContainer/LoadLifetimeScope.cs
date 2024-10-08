using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts.Time;
using Scripts.Scenes;
using Scripts.Chest;
using UnityEditor;
using Scripts.SaveLoadNamespace;
using Scripts.MoneyNamespace;

public sealed class LoadLifetimeScope : LifetimeScope
{
    [SerializeField]
    private TimeServiceConfig _timeServConfig;

    [SerializeField]
    private LoadingGame _loadingGame;

    [SerializeField]
    private GroupChestsConfig _groupChestsConfig;

    [SerializeField]
    private CurrencyConfig _currencyConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterTimeService(builder);

        RegisterSaveLoad(builder);

        RegisterSceneLoader(builder);
    }

    private void RegisterTimeService(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<TimeService>()
            .WithParameter(_timeServConfig)
            .AsSelf();
    }

    private void RegisterSaveLoad(IContainerBuilder builder)
    {
        builder.Register<SaveLoadStartFinishTime>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<SaveLoadChests>(Lifetime.Singleton)
            .WithParameter(_groupChestsConfig)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<SaveLoadMoney>(Lifetime.Singleton)
            .WithParameter(_currencyConfig)
            .AsImplementedInterfaces()
            .AsSelf();

        builder.Register<AppQuitManager>(Lifetime.Singleton);
    }

    private void RegisterSceneLoader(IContainerBuilder builder)
    {
        builder.RegisterComponent(_loadingGame);
    }
}