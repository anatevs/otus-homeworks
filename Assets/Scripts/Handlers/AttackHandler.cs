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

        else //if doesn't have weapon
        {
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
}