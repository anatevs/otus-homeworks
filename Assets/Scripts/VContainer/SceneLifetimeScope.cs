using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UIService _uiService;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterContlollers(builder);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<HeroListService>()
            .WithParameter(_uiService).AsSelf();
    }

    private void RegisterContlollers(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<HeroClickController>()
            .AsSelf();
    }
}
