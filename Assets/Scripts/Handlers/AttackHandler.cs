using UnityEngine;

public sealed class AttackHandler : BaseHandler<AttackEvent>
{
    private readonly int _backDamage = 1;

    public AttackHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(AttackEvent evnt)
    {
        HeroEntity hero = evnt.hero;
        HeroEntity target = evnt.target;

        if (hero.TryGet(out WeaponComponent weapon))
        {
            IEffect effect = weapon.ability.effect;
            effect.Hero = hero;
            effect.Target = target;
            EventBus.RaiseEvent(weapon.ability.effect);
        }
        else
        {
            EventBus.RaiseEvent(new DefaultDamageEvent(hero, target));
        }
    }
}