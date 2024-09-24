using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts;
using Scripts.Scenes;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private TimeServiceConfig _timeServConfig;

    [SerializeField]
    private LoadingGame _loadingGame;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterTimeService(builder);
        RegisterSceneLoader(builder);
    }

    private void RegisterTimeService(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<TimeService>()
            .WithParameter(_timeServConfig)
            .AsSelf();
    }

    private void RegisterSceneLoader(IContainerBuilder builder)
    {
        builder.RegisterComponent(_loadingGame);
    }
}