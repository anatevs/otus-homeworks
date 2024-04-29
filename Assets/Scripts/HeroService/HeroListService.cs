using System.Collections.Generic;
using UI;
using System;
using Audio;

public sealed class HeroListService : IDisposable
{
    public event Action<Team> OnTeamEmpty;

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
                entity.Add(new TeamInfoComponent(teamName, i++));

                heroList.Add(entity);
            }
            _entities.Add(teamName, new HeroEntityList(heroList));

        }
    }

    public void ClickHero(Team team, int id)
    {
        OnClickEntity?.Invoke(GetEntity(team, id));
    }

    public async void InitActive(Team team, int index)
    {
        _entities[team].OnNextMove();

        HeroView heroView = _uiService.GetRedPlayer().GetView(index);
        heroView.SetActive(true);

        if (heroView.TryGetComponent<HeroAudio>(out HeroAudio heroAudio))
        {
            await heroAudio.PlaySoundAsync(SoundType.StartTurn);
        }
    }

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

    public int GetValidCount(Team team)
    {
        return _entities[team].ValidCount;
    }

    public IEnumerable<HeroEntity> ValidEntities(Team team)
    {
        IReadOnlyList<int> validIndexes = GetValidIndexes(team);

        for (int i = 0; i < validIndexes.Count; i++)
        {
            HeroEntity entity = GetEntity(team, validIndexes[i]);
            yield return entity;
        }
    }

    public void PrepareNextMove(Team team)
    {
        _entities[team].OnNextMove();
    }

    public void RemoveHero(HeroEntity entity)
    {
        Team team = entity.Get<TeamInfoComponent>().team;
        _entities[team].OnRemove(entity);

        if (_entities[team].GetValidIndexes().Count == 0)
        {
            OnTeamEmpty?.Invoke(team);
        }
    }

    void IDisposable.Dispose()
    {
    }
}