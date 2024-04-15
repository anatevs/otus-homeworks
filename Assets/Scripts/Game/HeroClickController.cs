using System;
using UnityEngine;
using VContainer.Unity;

public sealed class HeroClickController : IInitializable, IDisposable
{
    private readonly HeroListService _heroListService;
    private readonly CurrentTeamData _teamData;
    private readonly EventBus _eventBus;

    private readonly int _backDamage = 1;

    public HeroClickController(HeroListService heroListService, CurrentTeamData teamData, EventBus eventBus)
    {
        _heroListService = heroListService;
        _teamData = teamData;
        _eventBus = eventBus;
    }

    void IInitializable.Initialize()
    {
        _heroListService.OnViewClicked += OnClickedHero;
    }

    private void OnClickedHero(HeroEntity clickedEntity)
    {
        Team team = clickedEntity.Get<TeamComponent>().value;
        //Debug.Log(team);
        if (team != _teamData.Enemy)
        {
            return;
        }
        else
        {
            HeroEntity playerHero = _heroListService.GetCurrentActive(_teamData.Player);
            _eventBus.RaiseEvent(new AttackEvent(clickedEntity, playerHero));
            _eventBus.RaiseEvent(new DealDamageEvent(playerHero, _backDamage));


        }
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnViewClicked -= OnClickedHero;
    }
}