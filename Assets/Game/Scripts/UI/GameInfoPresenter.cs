using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoPresenter : IGameInfoPresenter
{
    public event Action<int> OnHPChanged;
    public event Action<int> OnBulletUse;
    public event Action<int> OnBulletAdd;
    public event Action<int> OnDestroyZombie;

    public int HP => throw new NotImplementedException();

    public int BulletCount => throw new NotImplementedException();

    public int BulletCapacity => throw new NotImplementedException();

    public int Killed => throw new NotImplementedException();

    private readonly PlayerEntity _playerEntity;

    public GameInfoPresenter(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }

    private void ChangeHP(int newHP)
    {
        if (_playerEntity.TryGetComponent<HPComponent>(out HPComponent hpComponent))
        {
            
        }
    }

    //public void Dispose(){unsubscriptions...}
}
