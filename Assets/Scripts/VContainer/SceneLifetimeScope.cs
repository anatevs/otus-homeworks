using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UIService _uiService;

    private readonly Team _startTeam = Team.Red;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterEventBus(builder);
        RegisterPipeline(builder);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterComponent(_uiService);

        builder.RegisterEntryPoint<HeroListService>()
            .AsSelf();

        builder.Register<CurrentTeamData>(Lifetime.Singleton)
            .WithParameter(_startTeam);

        builder.RegisterEntryPoint<HeroServicePresenter>()
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

    private void RegisterPipeline(IContainerBuilder builder)
    {
        builder.Register<TurnPipeline>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PipelineInstaller>(Lifetime.Singleton);
    }
}