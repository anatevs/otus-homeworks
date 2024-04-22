using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourerEffectHandler : BaseHandler<DevourerEffect>
{
    HeroListService _heroListService;

    public DevourerEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(DevourerEffect evnt)
    {
        Team enemyTeam = evnt.Target.Get<InfoComponent>().team;
        int randomIndex = Random.Range(0, 4);
        Debug.Log($"random index {randomIndex}");
        EventBus.RaiseEvent(new DealDamageEvent(_heroListService.GetEntity(enemyTeam, randomIndex), evnt.ExtraDamage));
    }
}