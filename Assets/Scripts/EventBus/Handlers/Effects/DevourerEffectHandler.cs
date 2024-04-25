using System;
using System.Collections.Generic;
using UnityEngine;

public class DevourerEffectHandler : BaseHandler<DevourerEffect>
{
    private readonly HeroListService _heroListService;

    public DevourerEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(DevourerEffect evnt)
    {
        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, evnt.Target));

        Team enemyTeam = evnt.Target.Get<InfoComponent>().team;

        IReadOnlyList<int> enemyValidIndexes = _heroListService.GetValidIndexes(enemyTeam);
        int randomIndex = enemyValidIndexes[UnityEngine.Random.Range(0, enemyValidIndexes.Count)];

        EventBus.RaiseEvent(new DefaultDealDamageEvent(_heroListService.GetEntity(enemyTeam, randomIndex), evnt.ExtraDamage));

        Debug.Log($"Devourer effect: minus {evnt.ExtraDamage} to {randomIndex} enemy");
    }
}