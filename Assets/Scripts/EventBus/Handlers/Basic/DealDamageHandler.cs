public sealed class DealDamageHandler : BaseHandler<DealDamageEvent>
{
    public DealDamageHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(DealDamageEvent evnt)
    {
        HeroEntity entity = evnt.target;
        int damage = evnt.damage;

        if (entity.TryGet<ShieldComponent>(out ShieldComponent shield))
        {
            IDefenceEffect effect = shield.effect;
            effect.Target = evnt.target;
            EventBus.RaiseEvent(effect);
        }

        else
        {
            EventBus.RaiseEvent(new DefaultDealDamageEvent(evnt.target, evnt.damage));
        }
    }
}