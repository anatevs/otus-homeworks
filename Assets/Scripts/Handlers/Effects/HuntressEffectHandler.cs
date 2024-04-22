public class HuntressEffectHandler : BaseHandler<HuntressEffect>
{
    public HuntressEffectHandler(EventBus eventBus) : base(eventBus)
    {

    }

    protected override void RaiseEvent(HuntressEffect evnt)
    {
        int damage = evnt.Hero.Get<DamageComponent>().value;

        EventBus.RaiseEvent(new DealDamageEvent(evnt.Target, damage));
    }
}