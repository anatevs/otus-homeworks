public sealed class DestroyVisualHandler : BaseHandler<DestroyEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(DestroyEvent evnt)
    {
        _visualPipeline.AddTask(new DestroyVisualTask(_heroServiceView, evnt.entity));
    }
}