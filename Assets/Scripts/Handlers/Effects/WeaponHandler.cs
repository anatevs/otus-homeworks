using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler<T> : BaseHandler<T> where T : IEffect
{
    private readonly int _backDamage = 1;

    public WeaponHandler(EventBus eventBus) : base(eventBus)
    {
    }

    protected override void RaiseEvent(T evnt)
    {
        
    }

    private void MakeDefaultDamage(HeroEntity hero, HeroEntity target, int damage, int backDamage)
    {
        EventBus.RaiseEvent(new DealDamageEvent(hero, backDamage));
        EventBus.RaiseEvent(new DealDamageEvent(target, damage));
    }
}