using UnityEngine;

public class DefaultDealDamageHandler : BaseHandler<DefaultDealDamageEvent>
{
    public DefaultDealDamageHandler(EventBus eventBus) : base(eventBus)
    {

    }

    protected override void RaiseEvent(DefaultDealDamageEvent evnt)
    {
        HeroEntity entity = evnt.entity;
        int damage = evnt.damage;

        if (!(entity.TryGet(out HPComponent hpComponent)))
        {
            Debug.Log($"damage is not possible:" +
                $" no hp component on entity {entity}");
        }
        else
        {
            hpComponent.Value -= damage;
            entity.Set(hpComponent);

            if (hpComponent.Value == 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(entity));
            }
        }
    }
}