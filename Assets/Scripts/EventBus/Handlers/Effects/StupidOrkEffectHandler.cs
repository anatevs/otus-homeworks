using System.Collections.Generic;
using UnityEngine;

public sealed class StupidOrkEffectHandler : BaseHandler<StupidOrkEffect>
{
    private readonly HeroListService _heroListService;

    public StupidOrkEffectHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(StupidOrkEffect evnt)
    {
        HeroEntity target = evnt.Target;

        int random = Random.Range(0, 2);

        if (random == 1)
        {
            Team enemyTeam = evnt.Target.Get<TeamInfoComponent>().team;

            IReadOnlyList<int> enemyValidIndexes = _heroListService.GetValidIndexes(enemyTeam);
            int randomIndex = enemyValidIndexes[Random.Range(0, enemyValidIndexes.Count)];

            target = _heroListService.GetEntity(enemyTeam, randomIndex);
        }

        EventBus.RaiseEvent(new DefaultAttackEvent(evnt.Hero, target));

        Debug.Log($"Stupid ork effect: make damage to other " +
            $"enemy number {target.Get<TeamInfoComponent>().id}");
    }
}