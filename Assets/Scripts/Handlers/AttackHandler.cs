using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class AttackHandler : IInitializable, IDisposable
{
    private EventBus _eventBus;

    public AttackHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Initialize()
    {
        _eventBus.Subscribe<AttackEvent>(Attack);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<AttackEvent>(Attack);
    }

    private void Attack(AttackEvent evnt)
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
            _eventBus.RaiseEvent(new DealDamageEvent(target, damage.value));

            Debug.Log($"{hero.Get<TeamComponent>().value} {hero.name} " +
                $"attacked {target.Get<TeamComponent>().value} {target.name}");
        }
    }

}