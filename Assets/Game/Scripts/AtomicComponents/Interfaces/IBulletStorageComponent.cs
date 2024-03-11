using System;

public interface IBulletStorageComponent
{
    public event Action<int> OnStorageChanged;

    public int GetCurrentCount();
}