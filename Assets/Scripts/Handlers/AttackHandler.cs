using UnityEngine;

public sealed class AttackHandler : BaseHandler<AttackEvent>
{
    public AttackHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(AttackEvent evnt)
    {
        HeroEntity hero = evnt.hero;
        HeroEntity target = evnt.target;

        if (!(hero.TryGet(out DamageComponent damage)))
        {
            Debug.Log($"damage is not possible," +
                $" no damage component on entity {hero}");
        }
        else
        {
            EventBus.RaiseEvent(new DealDamageEvent(target, damage.value));

            //Debug.Log($"{hero.Get<TeamComponent>().value} {hero.name} " +
            //    $"attacked {target.Get<TeamComponent>().value} {target.name}");
        }
    }
}