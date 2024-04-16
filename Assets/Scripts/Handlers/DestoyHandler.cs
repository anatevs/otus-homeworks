using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public sealed class DestoyHandler : IInitializable, IDisposable
{
    private readonly EventBus _eventBus;
    private readonly HeroListService _heroListService;

    public DestoyHandler(EventBus eventBus, HeroListService heroListService)
    {
        _eventBus = eventBus;
        _heroListService = heroListService;
    }

    void IInitializable.Initialize()
    {
        _eventBus.Subscribe<DestroyEvent>(OnDestroy);
    }

    void IDisposable.Dispose()
    {
        _eventBus.Unsubscribe<DestroyEvent>(OnDestroy);
    }

    private void OnDestroy(DestroyEvent evnt)
    {
        HeroEntity entity = evnt.entity;

        _heroListService.RemoveHero(entity);

        entity.Set(new IsActiveComponent(false));

        entity.gameObject.SetActive(false);
    }
}