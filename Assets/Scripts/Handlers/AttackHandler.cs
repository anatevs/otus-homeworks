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

        if (hero.TryGet(out WeaponComponent weapon))
        {
            IAttackEffect effect = weapon.effect;
            effect.Hero = hero;
            effect.Target = target;
            EventBus.RaiseEvent(effect);
        }
        else
        {
            EventBus.RaiseEvent(new DefaultAttackEvent(hero, target));
        }
    }
}