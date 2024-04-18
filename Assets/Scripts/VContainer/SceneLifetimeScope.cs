using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UIService _uiService;

    [SerializeField]
    private HeroServiceView _heroServiceView;

    private readonly Team _startTeam = Team.Red;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterServices(builder);
        //RegisterContlollers(builder);
        RegisterEventBus(builder);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterComponent(_uiService);

        builder.RegisterEntryPoint<HeroListService>()
            //.WithParameter(_uiService)
            .AsSelf();

        builder.Register<CurrentTeamData>(Lifetime.Singleton)
            .WithParameter(_startTeam);

        builder.RegisterEntryPoint<HeroServicePresenter>().AsSelf();

        //builder.RegisterEntryPoint<HeroServiceView>()
        //    .WithParameter(_uiService).AsSelf();

        builder.RegisterComponent(_heroServiceView)
            //.WithParameter(_uiService)
            .AsImplementedInterfaces();
            //.AsSelf();
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
        builder.RegisterEntryPoint<DestoyHandler>();

        builder.RegisterEntryPoint<NextMoveHandler>();
    }
}