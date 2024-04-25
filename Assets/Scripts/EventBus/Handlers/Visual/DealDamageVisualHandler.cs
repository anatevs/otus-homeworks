public sealed class DealDamageVisualHandler : BaseHandler<DefaultDealDamageEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public DealDamageVisualHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(DefaultDealDamageEvent evnt)
    {
        _visualPipeline.AddTask(new DealDamageVisualTask(
            _heroServiceView,
            evnt.entity.Get<InfoComponent>(),
            evnt.entity.Get<HPComponent>().CurrentHP,
            evnt.entity.Get<DamageComponent>().value));
    }
}