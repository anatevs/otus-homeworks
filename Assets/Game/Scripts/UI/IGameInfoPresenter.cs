using System;

public interface IGameInfoPresenter
{
    event Action<int> OnHPChanged;
    event Action<int> OnBulletUse;
    event Action<int> OnBulletAdd;
    event Action<int> OnDestroyZombie;

    public int HP { get; }
    public int BulletCount { get; }
    public int BulletCapacity { get; }
    public int Killed { get; }
}