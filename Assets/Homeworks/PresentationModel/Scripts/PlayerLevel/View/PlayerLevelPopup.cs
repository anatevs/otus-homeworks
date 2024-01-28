using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelPopup : MonoBehaviour
    {
        [SerializeField]
        private Text _currentLevelText;

        [SerializeField]
        private Slider _XPSlider;

        [SerializeField]
        private Text _XPText;

        [SerializeField]
        private LevelUpButton _levelUpButtonScript;

        private IPlayerLevelPresenter _levelPresenter;

        public void Show(IPresenter presenter)
        {
            if (presenter is not IPlayerLevelPresenter levelPresenter)
            {
                throw new Exception($"{presenter} is not a {typeof(IPlayerLevelPresenter)} type");
            }

            _levelPresenter = levelPresenter;

            FillPlayerLevelInfo(_levelPresenter.CurrentXP, _levelPresenter.RequiredXP, _levelPresenter.CurrentLevel);

            _levelPresenter.OnXPChanged += SetCurrenXP;
            _levelUpButtonScript.AddListener(_levelPresenter.LevelUp);
            _levelPresenter.OnAvailableLevelUp += _levelUpButtonScript.SetButtonToActive;
            _levelPresenter.OnLevelChanged += FillPlayerLevelInfo;
        }

        private void FillPlayerLevelInfo(int currentXP, int requiredXP, int currentLevel)
        {
            _currentLevelText.text = _levelPresenter.LevelString;
            SetSliderMax(requiredXP);
            SetCurrenXP(currentXP, requiredXP);
            FillXPBarText(currentXP, requiredXP);
            _levelUpButtonScript.SetButtonToInactive();
        }

        private void SetSliderMax(int requiredXP)
        {
            _XPSlider.maxValue = requiredXP;
        }

        private void SetCurrenXP(int currentXP, int requiredXP)
        {
            _XPSlider.value = currentXP;
            FillXPBarText(currentXP, requiredXP);
        }

        private void FillXPBarText(int currentXP, int requiredXP)
        {
            _XPText.text = $"XP: {currentXP} / {requiredXP}";
        }

        public void Hide()
        {
            _levelPresenter.OnXPChanged -= SetCurrenXP;
            _levelUpButtonScript.RemoveListener(_levelPresenter.LevelUp);
            _levelPresenter.OnLevelChanged -= FillPlayerLevelInfo;
        }
    }
}