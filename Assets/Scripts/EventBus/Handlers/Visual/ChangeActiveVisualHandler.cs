public sealed class ChangeActiveVisualHandler : BaseHandler<ChangeActiveEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceView _heroServiceView;

    public ChangeActiveVisualHandler(EventBus eventBus, 
        AudioVisualPipeline visualPipeline, 
        HeroServiceView heroServiceView) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceView = heroServiceView;
    }

    protected override void RaiseEvent(ChangeActiveEvent evnt)
    {
        _visualPipeline.AddTask(
            new ChangeActiveVisualTask(_heroServiceView, 
            evnt.prevHero.Get<InfoComponent>(),
            evnt.currentHero.Get<InfoComponent>()));
    }
}
