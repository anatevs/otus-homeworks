public sealed class DestroyVisualHandler : BaseHandler<DestroyEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public DestroyVisualHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(DestroyEvent evnt)
    {
        _visualPipeline.AddTask(new DestroyVisualTask(_heroServiceView, evnt.entity));
    }
}