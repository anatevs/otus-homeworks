using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts;
using Scripts.Scenes;
using Scripts.Chest;
using UnityEditor;
using Scripts.SaveLoad;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private TimeServiceConfig _timeServConfig;

    [SerializeField]
    private LoadingGame _loadingGame;

    [SerializeField]
    private GroupChestsConfig _groupChestsConfig;

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

        builder.Register<AppInOutTimeService>(Lifetime.Singleton);
    }

    private void RegisterSaveLoad(IContainerBuilder builder)
    {
        var saveLoadChests = new SaveLoadChests(_groupChestsConfig);

        builder.RegisterComponent(saveLoadChests);


        var chestsData = saveLoadChests.Load();

        builder.RegisterComponent(chestsData);
    }

    private void RegisterSceneLoader(IContainerBuilder builder)
    {
        builder.RegisterComponent(_loadingGame);
    }
}