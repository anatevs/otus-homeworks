using System.Collections.Generic;
using UnityEngine;

public class StupidOrkEffectHandler : BaseHandler<StupidOrkEffect>
{
    private readonly HeroListService _heroListService;

    private readonly bool[] _choises = new bool[2] { true, false };

    public StupidOrkEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(StupidOrkEffect evnt)
    {
        HeroEntity target = evnt.Target;

        int random = Random.Range(0, _choises.Length);

        if (_choises[random] )
        {
            Team enemyTeam = evnt.Target.Get<InfoComponent>().team;

            IReadOnlyList<int> enemyValidIndexes = _heroListService.GetValidIndexes(enemyTeam);
            int randomIndex = enemyValidIndexes[Random.Range(0, enemyValidIndexes.Count)];

            target = _heroListService.GetEntity(enemyTeam, randomIndex);
        }

        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, target));
    }
}