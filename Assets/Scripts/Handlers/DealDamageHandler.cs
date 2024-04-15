using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class DealDamageHandler : IInitializable, IDisposable
{
    private EventBus _eventBus;

    public DealDamageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Initialize()
    {
        _eventBus.Subscribe<DealDamageEvent>(DealDamage);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<DealDamageEvent>(DealDamage);
    }

    private void DealDamage(DealDamageEvent evnt)
    {
        HeroEntity entity = evnt.entity;
        int damage = evnt.damage;

        if (!(entity.TryGet(out HPComponent hpComponent)))
        {
            Debug.Log($"damage is not possible," +
                $" no hp component on entity {entity}");
        }
        else
        {
            int hp = hpComponent.Value;
            Debug.Log($"{entity.Get<TeamComponent>().value} {entity.name} before damage hp: {hp}");

            hpComponent.Value = hp - damage;
            
            Debug.Log($"{entity.Get<TeamComponent>().value} {entity.name} hp: {hpComponent.Value}");
        }
    }
}