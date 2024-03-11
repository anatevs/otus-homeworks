using System;

public class GameInfoPresenter : IGameInfoPresenter
{
    public event Action<int> OnHPChanged;
    public event Action<int> OnBulletUse;
    public event Action<int> OnBulletAdd;
    public event Action<int> OnDestroyZombie;

    public int HP => _hpComponent.GetHP();

    public int BulletsCount => throw new NotImplementedException();

    public int BulletsCapacity => throw new NotImplementedException();

    public int Destroyed => throw new NotImplementedException();

    private readonly PlayerEntity _playerEntity;

    private readonly HPComponent _hpComponent;

    public GameInfoPresenter(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;

        _hpComponent = _playerEntity.GetComponentFromEntity<HPComponent>();

        _hpComponent.OnHPChanged += OnHPChanged;
    }


    public void Dispose()
    {
        _hpComponent.OnHPChanged -= OnHPChanged;
    }
}
