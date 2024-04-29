using UnityEngine;

public sealed class MediatorEffectHandler : BaseHandler<MediatorEffect>
{
    public MediatorEffectHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(MediatorEffect evnt)
    {
        int index = Random.Range(0, evnt.ValidCount);
        int counter = 0;
        foreach (HeroEntity entity in evnt.Teammates)
        {
            if (counter == index)
            {
                EventBus.RaiseEvent(new DefaultDealDamageEvent(
                    entity, -evnt.extraHP));
            }
            counter++;
        }

        Debug.Log($"Mediator effect: plus {evnt.extraHP} to {index} teammate");
    }
}