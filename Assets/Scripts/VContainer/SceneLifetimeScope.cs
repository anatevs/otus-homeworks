using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SceneLifetimeScope : LifetimeScope
{
    [SerializeField]
    private UIService _uiService;

    [SerializeField]
    private HeroServiceView _heroServiceView;

    private readonly Team _startTeam = Team.Red;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterServices(builder);
        RegisterEventBus(builder);
        RegisterPipeline(builder);
        RegisterVisualPipeline(builder);
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

        builder.RegisterComponent(_heroServiceView);
    }

    private void RegisterEventBus(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);

        builder.RegisterEntryPoint<ChangeActiveHandler>();
        builder.RegisterEntryPoint<AttackHandler>();
        builder.RegisterEntryPoint<DefaultDamageHandler>();
        builder.RegisterEntryPoint<DealDamageHandler>();
        builder.RegisterEntryPoint<DestoyHandler>();
        builder.RegisterEntryPoint<NextMoveHandler>();

        builder.RegisterEntryPoint<DevourerEffectHandler>();
        builder.RegisterEntryPoint<HuntressEffectHandler>();
        builder.RegisterEntryPoint<StupidOrkEffectHandler>();
        builder.RegisterEntryPoint<LordVampEffectHandler>();
    }

    private void RegisterPipeline(IContainerBuilder builder)
    {
        builder.Register<TurnPipeline>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PipelineInstaller>(Lifetime.Singleton);
    }

    private void RegisterVisualPipeline(IContainerBuilder builder)
    {
        builder.Register<VisualPipeline>(Lifetime.Singleton);

        builder.RegisterEntryPoint<ChangeActiveVisualHandler>();

        builder.RegisterEntryPoint<AttackVisualHandler>();

        builder.RegisterEntryPoint<DealDamageVisualHandler>();

        builder.RegisterEntryPoint<DestroyVisualHandler>();
    }
}