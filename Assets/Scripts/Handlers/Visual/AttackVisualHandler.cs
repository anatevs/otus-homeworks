public class AttackVisualHandler : BaseHandler<AttackEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(AttackEvent attackEvent)
    {
        _visualPipeline.AddTask(new AttackVisualTask(_heroServiceView, attackEvent.hero, attackEvent.target));
    }
}