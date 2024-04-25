public sealed class AttackVisualHandler : BaseHandler<AttackEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public AttackVisualHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(AttackEvent attackEvent)
    {
        _visualPipeline.AddTask(new AttackVisualTask(_heroServiceView, attackEvent.hero, attackEvent.target));
    }
}