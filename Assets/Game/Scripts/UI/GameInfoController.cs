using System;
using VContainer.Unity;

public class GameInfoController :
    IInitializable,
    IDisposable
{
    private readonly GameInfoModel _model;
    private readonly GameInfoView _view;

    public GameInfoController(GameInfoModel model, GameInfoView view)
    {
        _model = model;
        _view = view;
    }

    void IInitializable.Initialize()
    {
        _view.SetAllInfo(_model.HP,
            _model.BulletsCount,
            _model.BulletsCapacity,
            _model.Destroyed);

        _model.OnHPChanged += _view.SetHPText;
        _model.OnBulletStorageChanged += _view.SetBulletsInfo;
        _model.OnDestroyZombie += _view.SetDestroyedCount;
    }

    void IDisposable.Dispose()
    {
        _model.OnHPChanged -= _view.SetHPText;
        _model.OnBulletStorageChanged -= _view.SetBulletsInfo;
        _model.OnDestroyZombie -= _view.SetDestroyedCount;
    }

}