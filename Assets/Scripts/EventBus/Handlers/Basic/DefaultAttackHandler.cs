using UnityEngine;

public sealed class DefaultAttackHandler : BaseHandler<DefaultAttackEvent>
{
    private readonly int _backDamage = 1;

    public DefaultAttackHandler(EventBus eventBus) : base(eventBus) { }

    protected override void RaiseEvent(DefaultAttackEvent evnt)
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
            EventBus.RaiseEvent(new DealDamageEvent(hero, _backDamage));
            EventBus.RaiseEvent(new DealDamageEvent(target, damage.value));
        }
    }
}