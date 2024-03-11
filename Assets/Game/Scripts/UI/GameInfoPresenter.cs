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
        _hpComponent = _playerEntity.GetComponentFromEntity<HPComponent>();
        _bulletStorageComponent = _playerEntity.GetComponentFromEntity<BulletStorageComponent>();

        _hpComponent.OnHPChanged += ChangeHP;
        _bulletStorageComponent.OnStorageChanged += ChangeBulletStorage;
    }

    public void Dispose()
    {
        _hpComponent.OnHPChanged -= ChangeHP;
        _bulletStorageComponent.OnStorageChanged -= ChangeBulletStorage;
    }

    private void ChangeHP(int hp)
    {
        OnHPChanged?.Invoke(hp);
    }

    private void ChangeBulletStorage(int newBulletsCount)
    {
        OnBulletStorageChanged?.Invoke(newBulletsCount);
    }
}