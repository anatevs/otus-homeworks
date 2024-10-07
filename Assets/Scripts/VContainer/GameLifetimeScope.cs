using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts;
using Scripts.Scenes;
using Scripts.Chest;
using UnityEditor;
using Scripts.SaveLoadNamespace;
using Scripts.MoneyNamespace;

public sealed class GameLifetimeScope : LifetimeScope
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

        RegisterMoneyStorages(builder);

        RegisterSceneLoader(builder);
    }

    private void RegisterTimeService(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<TimeService>()
            .WithParameter(_timeServConfig)
            .AsSelf();

        //builder.Register<StartFinishTimeData>(Lifetime.Singleton);
    }

    private void RegisterSaveLoad(IContainerBuilder builder)
    {
        builder.Register<SaveLoadStartFinishTime>(Lifetime.Singleton)
            .AsImplementedInterfaces()
            .AsSelf();

        //var saveLoadChests = new SaveLoadChests(_groupChestsConfig);

        builder.Register<SaveLoadChests>(Lifetime.Singleton)
            .WithParameter(_groupChestsConfig)
            .AsImplementedInterfaces()
            .AsSelf();


        //var chestsData = saveLoadChests.Load();

        //builder.RegisterComponent(chestsData);

        builder.Register<AppQuitManager>(Lifetime.Singleton);
    }

    private void RegisterMoneyStorages(IContainerBuilder builder)
    {
        builder.Register<MoneyStoragesRepository>(Lifetime.Singleton)
            .WithParameter(_currencyConfig);
    }

    private void RegisterSceneLoader(IContainerBuilder builder)
    {
        builder.RegisterComponent(_loadingGame);
    }
}