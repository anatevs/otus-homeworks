using VContainer;
using VContainer.Unity;
using UnityEngine;
using Scripts;
using Scripts.Scenes;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    private TimeService _timeService;

    [SerializeField]
    private LoadingGame _loadingGame;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterTimeService(builder);
        RegisterSceneLoader(builder);
    }

    private void RegisterTimeService(IContainerBuilder builder)
    {
        builder.RegisterComponent(_timeService);
    }

    private void RegisterSceneLoader(IContainerBuilder builder)
    {
        builder.RegisterComponent(_loadingGame);
    }
}
