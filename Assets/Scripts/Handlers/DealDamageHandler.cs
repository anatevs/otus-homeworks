using System;
using UnityEngine;
using VContainer.Unity;

public class DealDamageHandler : IInitializable, IDisposable
{
    private EventBus _eventBus;

    public DealDamageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    void IInitializable.Initialize()
    {
        _eventBus.Subscribe<DealDamageEvent>(DealDamage);
    }

    void IDisposable.Dispose()
    {
        _eventBus.Unsubscribe<DealDamageEvent>(DealDamage);
    }

    private void DealDamage(DealDamageEvent evnt)
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
                _eventBus.RaiseEvent(new DestroyEvent(entity));
            }
        }
    }
}