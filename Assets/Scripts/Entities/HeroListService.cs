using System.Collections.Generic;
using UI;
using System;
using UnityEngine;
using VContainer.Unity;

public class HeroListService : IDisposable
{
    public event Action<HeroEntity> OnViewClicked;

    private readonly UIService _uiService;

    private readonly Dictionary<HeroView, HeroEntity> _viewsEntities = new();

    private readonly Dictionary<Team, HeroEntityList> _entities = new();

    private readonly Dictionary<Team, HeroListView> _views = new();

    public HeroListService(UIService uiService)
    {
        _uiService = uiService;

        InitEntities();
    }

    void IDisposable.Dispose()
    {
        _uiService.GetRedPlayer().OnHeroClicked -= OnClickedEvent;
        _uiService.GetBluePlayer().OnHeroClicked -= OnClickedEvent;
    }


    private void InitEntities()
    {
        foreach (Team teamName in Enum.GetValues(typeof(Team)))
        {
            HeroListView heroListView;

            if (teamName == Team.Red)
            {
                heroListView = _uiService.GetRedPlayer();
            }
            else
            {
                heroListView = _uiService.GetBluePlayer();
            }

            List<HeroEntity> heroList = new();

            foreach (HeroView heroView in heroListView.GetViews())
            {
                HeroEntity entity = heroView.GetComponent<HeroEntity>();
                entity.Add(new TeamComponent(teamName));

                heroList.Add(entity);
                _viewsEntities.Add(heroView, entity);
            }
            _entities.Add(teamName, new HeroEntityList(heroList));
            _views.Add(teamName, heroListView);

            heroListView.OnHeroClicked += OnClickedEvent;
        }

        //maybe need to init 1st active in some other class...
        _entities[Team.Red].Get(0).Set(new IsActiveComponent(true));
        _entities[Team.Red].OnNextMove();

        _uiService.GetRedPlayer().GetView(0).SetActive(true);
    }

    private void OnClickedEvent(HeroView heroView)
    {
        OnViewClicked.Invoke(_viewsEntities[heroView]);
    }

    public HeroEntityList GetEntityList(Team team)
    {
        return _entities[team];
    }

    public HeroEntity GetCurrentActive(Team team)
    {
        return _entities[team].GetCurrentActive();
    }

    public HeroEntity GetEntity(Team team, int index)
    {
        return _entities[team].Get(index);
    }

    public HeroView GetView(Team team, int index)
    {
        return _views[team].GetView(index);
    }

    public void RemoveHero(HeroEntity hero)
    {
        Team team = hero.Get<TeamComponent>().value;
        _entities[team].OnRemove(hero);
    }

    public void PrepareNextMove(Team team)
    {
        _entities[team].OnNextMove();
    }
}