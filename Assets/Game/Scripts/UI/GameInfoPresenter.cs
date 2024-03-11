using System;
using UnityEngine;
using VContainer.Unity;

public class GameInfoPresenter : IGameInfoPresenter, 
    IStartable,
    IDisposable
{
    public event Action<int> OnHPChanged;
    public event Action<int> OnBulletStorageChanged;
    public event Action<int> OnDestroyZombie;

    public int HP => _hpComponent.GetHP();

    public int BulletsCount => _bulletStorageComponent.GetCurrentCount();

    public int Destroyed => 0;

    private readonly PlayerEntity _playerEntity;

    private HPComponent _hpComponent;

    private BulletStorageComponent _bulletStorageComponent;

    public GameInfoPresenter(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }

    void IStartable.Start()
    {
        Debug.Log("presenter init");
        _hpComponent = _playerEntity.GetComponentFromEntity<HPComponent>();
        _bulletStorageComponent = _playerEntity.GetComponentFromEntity<BulletStorageComponent>();

        _hpComponent.OnHPChanged += OnHPChanged;
        _bulletStorageComponent.OnStorageChanged += OnBulletStorageChanged;
    }

    public void Dispose()
    {
        _hpComponent.OnHPChanged -= OnHPChanged;
        _bulletStorageComponent.OnStorageChanged -= OnBulletStorageChanged;
    }
}