using UnityEngine;

public class DealDamageHandler : BaseHandler<DealDamageEvent>
{
    public DealDamageHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(DealDamageEvent evnt)
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

            Debug.Log($"{entity.Get<TeamComponent>().value} {entity.name} hp: {entity.Get<HPComponent>().Value}");

            if (hpComponent.Value == 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(entity));
            }
        }
    }
}