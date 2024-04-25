using UnityEngine;

public sealed class MediatorEffectHandler : BaseHandler<MediatorEffect>
{
    public MediatorEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(MediatorEffect evnt)
    {
        int index = Random.Range(0, evnt.TeammateEntities.Count);
        EventBus.RaiseEvent(new DefaultDealDamageEvent(
            evnt.TeammateEntities[index], -evnt.extraHP));

        Debug.Log($"Mediator effect: plus {evnt.extraHP} to {index} teammate");
    }
}