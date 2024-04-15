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
        RegisterEventBus(builder);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<HeroListService>()
            .WithParameter(_uiService).AsSelf();

        builder.Register<CurrentTeamData>(Lifetime.Singleton);
    }

    private void RegisterContlollers(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<HeroClickController>()
            .AsSelf();
    }

    private void RegisterEventBus(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);

        builder.RegisterEntryPoint<AttackHandler>();
        builder.RegisterEntryPoint<DealDamageHandler>();
        //builder.RegisterEntryPoint<NextMoveHandler>();
    }
}