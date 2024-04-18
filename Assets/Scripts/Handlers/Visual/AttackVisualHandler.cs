public class AttackVisualHandler : BaseHandler<AttackEvent>
{
    VisualPipeline _visualPipeline;

    public AttackVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void RaiseEvent(AttackEvent attackEvent)
    {

        //_heroListService.Attack(attackEvent.hero, attackEvent.target);
    }
}