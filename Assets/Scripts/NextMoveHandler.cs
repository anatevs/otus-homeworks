using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer.Unity;

public class NextMoveHandler : IInitializable, IDisposable
{
    private EventBus _eventBus;
    private HeroListService _heroListService;
    private NextMoveEvent _nextMoveEvent;

    public NextMoveHandler(EventBus eventBus, HeroListService heroListService, NextMoveEvent nextMoveEvent)
    {
        _eventBus = eventBus;
        _heroListService = heroListService;
        _nextMoveEvent = nextMoveEvent;
    }

    public void Initialize()
    {
        _eventBus.Subscribe<NextMoveEvent>(RaiseEvent);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<NextMoveEvent>(RaiseEvent);
    }

    private void RaiseEvent(NextMoveEvent nextMoveEvent)
    {
        Team team = nextMoveEvent.playingTeam;
        //int currIndex = _heroListService.GetEntityList(team).GetCurrentActiveIndex();

        //HeroEntity currentHero = _heroListService.GetEntity(team, currIndex);
        //HeroView heroView = _heroListService.GetView(team, currIndex);

        HeroEntity currentHero = _heroListService.GetCurrentActive(team);

        currentHero.Set(new IsActiveComponent(true));


    }
}
