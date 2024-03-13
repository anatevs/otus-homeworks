using System;
using UnityEngine;
using VContainer.Unity;

public sealed class GameInfoPresenter : IGameInfoPresenter, 
    IStartable,
    IDisposable
{
    public event Action<int> OnHPChanged;
    public event Action<int> OnBulletStorageChanged;
    public event Action<int> OnDestroyZombie;

    public int HP => _hpComponent.HP;

    public int BulletsCount => _bulletStorageComponent.GetCurrentCount();

    public int Destroyed => 0;

    private readonly PlayerEntity _playerEntity;

    private readonly ZombieSystem _zombieSystem;

    private HPComponent _hpComponent;

    private BulletStorageComponent _bulletStorageComponent;

    public GameInfoPresenter(PlayerEntity playerEntity, ZombieSystem zombieSystem)
    {
        _playerEntity = playerEntity;
        _zombieSystem = zombieSystem;
    }

    void IStartable.Start()
    {
        _hpComponent = _playerEntity.GetComponentFromEntity<HPComponent>();
        _bulletStorageComponent = _playerEntity.GetComponentFromEntity<BulletStorageComponent>();

        _hpComponent.OnHPChanged += ChangeHP;
        _bulletStorageComponent.OnStorageChanged += ChangeBulletStorage;
        _zombieSystem.OnDestroyZombie += ChangeDestroyedCount;
    }

    public void Dispose()
    {
        _hpComponent.OnHPChanged -= ChangeHP;
        _bulletStorageComponent.OnStorageChanged -= ChangeBulletStorage;
        _zombieSystem.OnDestroyZombie -= ChangeDestroyedCount;
    }

    private void ChangeHP(int hp)
    {
        OnHPChanged?.Invoke(hp);
    }

    private void ChangeBulletStorage(int newBulletsCount)
    {
        OnBulletStorageChanged?.Invoke(newBulletsCount);
    }

    private void ChangeDestroyedCount(int destroyedCount)
    {
        OnDestroyZombie?.Invoke(destroyedCount);
    }
}