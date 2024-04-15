using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public sealed class DestoyHandler : IInitializable, IDisposable
{
    private readonly EventBus _eventBus;

    public DestoyHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
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
        entity.Set(new IsActiveComponent(false));

        entity.gameObject.SetActive(false);
    }
}