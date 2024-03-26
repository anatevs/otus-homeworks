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

    public void SetAllInfo(int hp, int bulletsCount, int bulletsCapacity, int destroyedCount)
    {
        SetHPText(hp);
        SetBulletsInfo(bulletsCount, bulletsCapacity);
        SetDestroyedCount(destroyedCount);
    }

    public void SetHPText(int hp)
    {
        _hpText.text = $"HIT POINTS: {hp}";
    }

    public void SetBulletsInfo(int bulletsCount, int bulletsCapacity)
    {
        _bulletsText.text = $"BULLETS: {bulletsCount}/{bulletsCapacity}";
    }

    public void SetDestroyedCount(int destroyedCount)
    {
        _destroyedText.text = $"KILLS: {destroyedCount}";
    }
}