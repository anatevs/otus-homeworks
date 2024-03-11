using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoView : MonoBehaviour
{
    [SerializeField]
    private Text _hpText;

    [SerializeField]
    private Text _bulletsText;

    [SerializeField]
    private Text _killsText;

    private GameInfoPresenter _gameInfoPresenter;

    public void Show(IGameInfoPresenter gameInfoPresenter)
    {
        _gameInfoPresenter = (GameInfoPresenter)gameInfoPresenter;

        FillAllInfo(_gameInfoPresenter.HP,
            _gameInfoPresenter.BulletsCount,
            _gameInfoPresenter.BulletsCapacity,
            _gameInfoPresenter.Destroyed);
    }

    private void FillAllInfo(int hp, int bulletsCount, int bulletsCapacity, int destroyedCount)
    {
        FillHPText(hp);
        FillBulletsCount(bulletsCount, bulletsCapacity);
        FillDestroyedCount(destroyedCount);
    }

    private void FillHPText(int hp)
    {
        _hpText.text = $"HIT POINTS: {hp}";
    }

    private void FillBulletsCount(int bulletsCount, int bulletsCapacity)
    {
        _bulletsText.text = $"BULLETS: {bulletsCount}/{bulletsCapacity}";
    }

    private void FillDestroyedCount(int destroyedCount)
    {
        _killsText.text = $"KILLS: {destroyedCount}";
    }
}
