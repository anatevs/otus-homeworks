using System;

public class BulletStorageComponent : IBulletStorageComponent
{
    public event Action<int> OnStorageChanged 
    {
        add { _bulletStorage.Subscribe(value); }
        remove { _bulletStorage.Unsubscribe(value); }
    }

    private AtomicVariable<int> _bulletStorage;

    public BulletStorageComponent(AtomicVariable<int> bulletStorage)
    {
        _bulletStorage = bulletStorage;
    }

    public int GetCurrentCount()
    {
        return _bulletStorage.Value;
    }
}