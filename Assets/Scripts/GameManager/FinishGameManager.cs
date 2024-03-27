using System;
using UnityEngine;
using VContainer.Unity;

public class FinishGameManager :
    IPostInitializable,
    IDisposable
{
    //private readonly PlayerEntity _playerEntity;
    private readonly GameManager _gameManager;

    public FinishGameManager(GameManager gameManager)
    {
        //_playerEntity = playerEntity;
        _gameManager = gameManager;
    }

    void IPostInitializable.PostInitialize()
    {
        //_playerEntity.GetEntityComponent<DeathComponent>().OnDeath += MakeFinishGame;
        //_playerEntity.GetEntityComponent<DestroyedComponent>().OnDestroyed += MakeEndGame;
    }

    void IDisposable.Dispose()
    {
        //_playerEntity.GetEntityComponent<DeathComponent>().OnDeath -= MakeFinishGame;
        //_playerEntity.GetEntityComponent<DestroyedComponent>().OnDestroyed -= MakeEndGame;
    }

    private void MakeFinishGame(bool isPlayerDead)
    {
        if (isPlayerDead)
        {
            _gameManager.FinishGame();
        }
    }

    private void MakeEndGame(bool isPlayerDestroyed)
    {
        if (isPlayerDestroyed)
        {
            _gameManager.EndGame();
            Time.timeScale = 0;
        }
    }
}