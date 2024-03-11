using System;

public interface IGameInfoPresenter
{
    event Action<int> OnHPChanged;
    event Action<int> OnBulletUse;
    event Action<int> OnBulletAdd;
    event Action<int> OnDestroyZombie;

    public int HP { get; }
    public int BulletsCount { get; }
    public int BulletsCapacity { get; }
    public int Destroyed { get; }
}