using System;
using UnityEngine;
using VContainer.Unity;

public class GameInfoModel :
    IInitializable,
    IDisposable
{
    public event Action<int> OnHPChanged;
    public event Action<int, int> OnBulletStorageChanged;
    public event Action<int> OnDestroyZombie;

    public int HP => _hpComponent.HP;

    public int BulletsCount => _bulletStorageComponent.GetCurrentCount();

    public int BulletsCapacity => _bulletsCapacity;

    public int Destroyed => 0;

    private HPComponent _hpComponent;
    private BulletStorageComponent _bulletStorageComponent;

    private int _prevBulletCount;
    private int _bulletsCapacity;

    private readonly PlayerEntity _playerEntity;
    private readonly ZombieSystem _zombieSystem;

    public GameInfoModel(PlayerEntity playerEntity, ZombieSystem zombieSystem)
    {
        _playerEntity = playerEntity;
        _zombieSystem = zombieSystem;
    }

    void IInitializable.Initialize()
    {
        _hpComponent = _playerEntity.GetEntityComponent<HPComponent>();
        _bulletStorageComponent = _playerEntity.GetEntityComponent<BulletStorageComponent>();

        _hpComponent.OnHPChanged += UpdateHPInfo;
        _bulletStorageComponent.OnStorageChanged += UpdateBulletsInfo;
        _zombieSystem.OnDestroyZombie += UpdateDestroyedInfo;

        _prevBulletCount = BulletsCount;
        _bulletsCapacity = BulletsCount;
    }

    void IDisposable.Dispose()
    {
        _hpComponent.OnHPChanged -= UpdateHPInfo;
        _bulletStorageComponent.OnStorageChanged -= UpdateBulletsInfo;
        _zombieSystem.OnDestroyZombie -= UpdateDestroyedInfo;
    }

    private void UpdateHPInfo(int hp)
    {
        OnHPChanged.Invoke(hp);
    }

    private void UpdateBulletsInfo(int newBulletCount)
    {
        int deltaBullets = newBulletCount - _prevBulletCount;
        _prevBulletCount = newBulletCount;

        _bulletsCapacity += Mathf.Max(0, deltaBullets);

        OnBulletStorageChanged.Invoke(newBulletCount, _bulletsCapacity);
    }

    private void UpdateDestroyedInfo(int destroyedCount)
    {
        OnDestroyZombie.Invoke(destroyedCount);
    }
}