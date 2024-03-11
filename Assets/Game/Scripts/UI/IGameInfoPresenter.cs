using System;

public interface IGameInfoPresenter
{
    event Action<int> OnHPChanged;
    event Action<int> OnBulletStorageChanged;
    event Action<int> OnDestroyZombie;

    public int HP { get; }
    public int BulletsCount { get; }
    public int Destroyed { get; }
}