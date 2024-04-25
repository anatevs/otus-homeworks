using System;
using UnityEngine;
using VContainer.Unity;

public sealed class GameManager : IInitializable, IPostStartable, IDisposable
{
    public event Action OnGameFinished;

    private readonly CurrentTeamData _teamData;
    private readonly HeroListService _heroListService;

    public GameManager(CurrentTeamData teamData, HeroListService heroListService)
    {
        _teamData = teamData;
        _heroListService = heroListService;
    }

    private readonly int _startIndex = 0;

    void IInitializable.Initialize()
    {
        _heroListService.OnTeamEmpty += FinishGame;
    }

    void IPostStartable.PostStart()
    {
        _heroListService.InitActive(_teamData.Player, _startIndex);
    }

    public void FinishGame(Team lostTeam)
    {
        OnGameFinished?.Invoke();
        Debug.Log($"Game is over! The {lostTeam} team is lost");
    }

    void IDisposable.Dispose()
    {
        _heroListService.OnTeamEmpty -= FinishGame;
    }
}