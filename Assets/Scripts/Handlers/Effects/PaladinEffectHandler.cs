public class PaladinEffectHandler : BaseHandler<PaladinEffect>
{
    public PaladinEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(PaladinEffect evnt)
    {
        evnt.Target.Remove<ShieldComponent>();
    }
}