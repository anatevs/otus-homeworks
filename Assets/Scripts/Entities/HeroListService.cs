using System.Collections.Generic;
using UI;
using System;
using UnityEngine;
using VContainer.Unity;

public class HeroListService : IDisposable
{
    public Action<InfoComponent> OnClicked;

    public Action<InfoComponent> OnDestroy;

    public Action<InfoComponent, bool> OnSetActive;

    public Action<InfoComponent, int, int> OnChangeHP;



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

            int i = 0;
            foreach (HeroView heroView in heroListView.GetViews())
            {
                HeroEntity entity = heroView.GetComponent<HeroEntity>();
                entity.Add(new TeamComponent(teamName));
                entity.Add(new IDComponent(i++));

                entity.Add(new InfoComponent(teamName, i));

                heroList.Add(entity);
                _viewsEntities.Add(heroView, entity);
            }
            _entities.Add(teamName, new HeroEntityList(heroList));
            _views.Add(teamName, heroListView);

            heroListView.OnHeroClicked += OnClickedEvent;
        }

        //maybe need to init 1st active in some other class...
        _entities[Team.Red].Get(0).Set(new IsActiveComponent(true));

        //SetActive(Team.Red, 0, true);
        _entities[Team.Red].OnNextMove();

        _uiService.GetRedPlayer().GetView(0).SetActive(true);
    }

    private void OnClickedEvent(HeroView heroView)
    {
        OnViewClicked?.Invoke(_viewsEntities[heroView]);
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

    public void SetActive(InfoComponent info, bool isActive)
    {
        HeroEntity entity = GetEntity(info.team, info.id);
        entity.Set(new IsActiveComponent(isActive));

        //if (isActive)
        //{
        //    _entities[team].OnNextMove();
        //}

        OnSetActive?.Invoke(info, isActive);
    }

    public void OnHPChanged(InfoComponent info, int hp)
    {
        HeroEntity entity = GetEntity(info.team, info.id);

        OnChangeHP?.Invoke(info, hp, entity.Get<DamageComponent>().value);
    }

    public void RemoveHero(HeroEntity entity)
    {
        Team team = entity.Get<TeamComponent>().value;
        _entities[team].OnRemove(entity);

        OnDestroy?.Invoke(entity.Get<InfoComponent>());
    }

    public void PrepareNextMove(Team team)
    {
        _entities[team].OnNextMove();
    }
}