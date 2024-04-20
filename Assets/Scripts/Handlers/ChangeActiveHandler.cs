public sealed class ChangeActiveHandler : BaseHandler<ChangeActiveEvent>
{
    public ChangeActiveHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(ChangeActiveEvent evnt)
    {
        evnt.prevHero.Set(new IsActiveComponent(false));
        evnt.currentHero.Set(new IsActiveComponent(true));
    }
}