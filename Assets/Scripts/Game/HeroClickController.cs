using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class HeroClickController : IInitializable, IDisposable
{
    private HeroListService _heroListService;

    [Inject]
    public void Construct(HeroListService heroListService)
    {
        _heroListService = heroListService;
    }

    void IInitializable.Initialize()
    {
        _heroListService.OnViewClicked += OnClickedHero;
    }

    private void OnClickedHero(HeroEntity entity)
    {
        Debug.Log(entity);
        //if clicked on enemy's team entity
        // _eventBus.Add(new AttackEvent(player, target));
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnViewClicked -= OnClickedHero;
    }
}
