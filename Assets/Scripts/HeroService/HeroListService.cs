using System.Collections.Generic;
using UI;
using System;
using UnityEngine;
using VContainer.Unity;

public sealed class HeroListService : IDisposable
{
    public event Action<InfoComponent> OnDestroy;

    public event Action<InfoComponent, bool> OnSetActive;

    public event Action<InfoComponent, InfoComponent> OnAttack;

    public event Action<InfoComponent, int, int> OnChangeHP;

    public event Action<HeroEntity> OnClickEntity;


    private readonly UIService _uiService;

    private readonly Dictionary<Team, HeroEntityList> _entities = new();

    public HeroListService(UIService uiService)
    {
        _uiService = uiService;

        InitEntities();
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
                entity.Add(new InfoComponent(teamName, i++));

                heroList.Add(entity);
            }
            _entities.Add(teamName, new HeroEntityList(heroList));

        }

        //maybe need to init 1st active in some other class...
        _entities[Team.Red].Get(0).Set(new IsActiveComponent(true));

        //SetActive(Team.Red, 0, true);
        _entities[Team.Red].OnNextMove();

        _uiService.GetRedPlayer().GetView(0).SetActive(true);
    }

    public void ClickHero(Team team, int id)
    {
        OnClickEntity?.Invoke(GetEntity(team, id));
    }

    //public HeroEntityList GetEntityList(Team team)
    //{
    //    return _entities[team];
    //}

    public HeroEntity GetCurrentActive(Team team)
    {
        return _entities[team].GetCurrentActive();
    }

    public HeroEntity GetEntity(Team team, int index)
    {
        return _entities[team].Get(index);
    }

    public IReadOnlyList<int> GetValidIndexes(Team team)
    {
        return _entities[team].GetValidIndexes();
    }

    public void SetActive(InfoComponent info, bool isActive)
    {
        HeroEntity entity = GetEntity(info.team, info.id);
        entity.Set(new IsActiveComponent(isActive));

        OnSetActive?.Invoke(info, isActive);
    }

    public void RemoveHero(HeroEntity entity)
    {
        Team team = entity.Get<InfoComponent>().team;
        _entities[team].OnRemove(entity);

        //OnDestroy?.Invoke(entity.Get<InfoComponent>());
    }

    public void Attack(HeroEntity hero, HeroEntity target)
    {
        OnAttack?.Invoke(hero.Get<InfoComponent>(),
            target.Get<InfoComponent>());
    }

    public void ChangeHP(InfoComponent info, int hp)
    {
        HeroEntity entity = GetEntity(info.team, info.id);

        OnChangeHP?.Invoke(info, hp, entity.Get<DamageComponent>().value);
    }

    public void PrepareNextMove(Team team)
    {
        _entities[team].OnNextMove();
    }

    void IDisposable.Dispose()
    {
    }
}