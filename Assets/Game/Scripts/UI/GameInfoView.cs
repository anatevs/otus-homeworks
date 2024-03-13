using UnityEngine;
using TMPro;

public sealed class GameInfoView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _hpText;

    [SerializeField]
    private TMP_Text _bulletsText;

    [SerializeField]
    private TMP_Text _destroyedText;

    private GameInfoPresenter _gameInfoPresenter;

    private int _prevBulletCount;
    private int _bulletsCapacity;

    public void Show(IGameInfoPresenter gameInfoPresenter)
    {
        _gameInfoPresenter = (GameInfoPresenter)gameInfoPresenter;

        _prevBulletCount = _gameInfoPresenter.BulletsCount;
        _bulletsCapacity = _gameInfoPresenter.BulletsCount;

        FillAllInfo(_gameInfoPresenter.HP,
            _gameInfoPresenter.BulletsCount,
            _gameInfoPresenter.Destroyed);

        _gameInfoPresenter.OnHPChanged += FillHPText;
        _gameInfoPresenter.OnBulletStorageChanged += FillBulletsCount;
        _gameInfoPresenter.OnDestroyZombie += FillDestroyedCount;
    }

    public void Hide()
    {
        _gameInfoPresenter.OnHPChanged -= FillHPText;
        _gameInfoPresenter.OnBulletStorageChanged -= FillBulletsCount;
        _gameInfoPresenter.OnDestroyZombie -= FillDestroyedCount;
    }

    private void FillAllInfo(int hp, int bulletsCount, int destroyedCount)
    {
        FillHPText(hp);
        FillBulletsCount(bulletsCount);
        FillDestroyedCount(destroyedCount);
    }

    private void FillHPText(int hp)
    {
        _hpText.text = $"HIT POINTS: {hp}";
    }

    private void FillBulletsCount(int bulletsCount)
    {
        UpdateBulletCapacity(bulletsCount);
        _bulletsText.text = $"BULLETS: {bulletsCount}/{_bulletsCapacity}";
    }

    private void FillDestroyedCount(int destroyedCount)
    {
        _destroyedText.text = $"KILLS: {destroyedCount}";
    }

    private void UpdateBulletCapacity(int newBulletCount)
    {
        int deltaBullets = newBulletCount - _prevBulletCount;
        _prevBulletCount = newBulletCount;
        if (deltaBullets < 0)
        {
            return;
        }
        else
        {
            _bulletsCapacity += deltaBullets;
        }
    }
}