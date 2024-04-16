using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer.Unity;

public class NextMoveHandler : IInitializable, IDisposable
{
    private readonly EventBus _eventBus;
    private readonly HeroListService _heroListService;
    private readonly CurrentTeamData _teamData;

    public NextMoveHandler(EventBus eventBus, HeroListService heroListService, CurrentTeamData teamData)
    {
        _eventBus = eventBus;
        _heroListService = heroListService;
        _teamData = teamData;
    }

    void IInitializable.Initialize()
    {
        _eventBus.Subscribe<NextMoveEvent>(RaiseEvent);
    }

    void IDisposable.Dispose()
    {
        _eventBus.Unsubscribe<NextMoveEvent>(RaiseEvent);
    }

    private void RaiseEvent(NextMoveEvent nextMoveEvent)
    {
        HeroEntity prevPlayer = nextMoveEvent.prevPlayer;
        prevPlayer.Set(new IsActiveComponent(false));

        _teamData.SwitchTeams();
        Team currentTeam = _teamData.Player;

        Debug.Log(currentTeam);

        _heroListService.PrepareNextMove(currentTeam);
        HeroEntity currentHero = _heroListService.GetCurrentActive(currentTeam);

        currentHero.Set(new IsActiveComponent(true));
    }
}
