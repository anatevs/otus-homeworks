using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class ElectroEffectHandler : BaseHandler<ElectroEffect>
{
    private readonly HeroListService _heroListService;

    public ElectroEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(ElectroEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultDealDamageEvent(evnt.Target, evnt.Damage));

        foreach (Team team in Enum.GetValues(typeof(Team)))
        {
            IReadOnlyList<HeroEntity> entities = _heroListService.GetValidEntities(team);
            for (int i = 0; i < entities.Count; i++)
            {
                EventBus.RaiseEvent(new DefaultDealDamageEvent(entities[i], evnt.extraDamage));
            }
        }

        Debug.Log($"Electo effect: extra damage {evnt.extraDamage} to all game heros");
    }
}