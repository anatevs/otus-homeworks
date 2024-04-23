public class IcyWizardEffectHandler : BaseHandler<IcyWizardEffect>
{
    private readonly HeroListService _heroListService;

    public IcyWizardEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(IcyWizardEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, evnt.Target));

        evnt.Target.Add(new FreezeComponent());
    }
}