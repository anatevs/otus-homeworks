using System;
using UnityEngine;
using VContainer.Unity;

public class FinishGameController : 
    //IInitializable,
    IPostInitializable,
    IDisposable
{
    private readonly PlayerEntity _playerEntity;
    private readonly GameManager _gameManager;

    public FinishGameController(PlayerEntity playerEntity, GameManager gameManager)
    {
        _playerEntity = playerEntity;
        _gameManager = gameManager;
    }

    public void PostInitialize()
    {
        _playerEntity.GetComponentFromEntity<DeathComponent>().OnDeath += MakeFinishGame;
    }

    public void Dispose()
    {
        _playerEntity.GetComponentFromEntity<DeathComponent>().OnDeath -= MakeFinishGame;
    }

    private void MakeFinishGame(bool isPlayerDead)
    {
        if (isPlayerDead)
        {
            _gameManager.FinishGame();
        }
    }
}