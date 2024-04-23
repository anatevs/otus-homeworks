using System.Collections;
public class DealDamageVisualHandler : BaseHandler<DefaultDealDamageEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public DealDamageVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(DefaultDealDamageEvent evnt)
    {
        _visualPipeline.AddTask(new DealDamageVisualTask(
            _heroServiceView,
            evnt.entity.Get<InfoComponent>(),
            evnt.entity.Get<HPComponent>().Value,
            evnt.entity.Get<DamageComponent>().value));
    }
}