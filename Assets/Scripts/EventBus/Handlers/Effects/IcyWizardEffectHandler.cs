using UnityEngine;

public sealed class IcyWizardEffectHandler : BaseHandler<IcyWizardEffect>
{
    public IcyWizardEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(IcyWizardEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, evnt.Target));

        evnt.Target.Add(new FreezeComponent());

        Debug.Log($"IcyWizard effect: added freeze to {evnt.Target}");
    }
}