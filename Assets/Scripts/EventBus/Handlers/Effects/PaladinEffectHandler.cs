using UnityEngine;

public sealed class PaladinEffectHandler : BaseHandler<PaladinEffect>
{
    public PaladinEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(PaladinEffect evnt)
    {
        evnt.Target.Remove<ShieldComponent>();
        Debug.Log($"Paladin effect: no damage at 1st attack");
    }
}