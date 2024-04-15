using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class HeroClickController : IInitializable, IDisposable
{
    private HeroListService _heroListService;
    private CurrentTeamData _teamData;
    private EventBus _eventBus;

    private readonly int _returnDamage = 1;

    [Inject]
    public void Construct(HeroListService heroListService, CurrentTeamData teamData, EventBus eventBus)
    {
        _heroListService = heroListService;
        _teamData = teamData;
        _eventBus = eventBus;
    }

    void IInitializable.Initialize()
    {
        _heroListService.OnViewClicked += OnClickedHero;
    }

    private void OnClickedHero(HeroEntity entity)
    {
        Team team = entity.Get<TeamComponent>().value;
        if (team != _teamData.Enemy)
        {
            return;
        }
        else
        {
            HeroEntity playerHero = _heroListService.GetCurrentActive(team);
            _eventBus.RaiseEvent(new AttackEvent(entity, playerHero.Get<DamageComponent>().value));
            _eventBus.RaiseEvent(new DealDamageEvent(playerHero, _returnDamage));
        }
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnViewClicked -= OnClickedHero;
    }
}