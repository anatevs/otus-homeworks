using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfoPopup : MonoBehaviour
    {
        [SerializeField]
        private Transform _characterStatsTransform;

        [SerializeField]
        private StatView _statViewPrefab;

        [SerializeField]
        private StatsContentArea _statsContentArea;

        private Dictionary<string, StatView> _statsDict = new Dictionary<string, StatView>();

        private ICharacterInfoPresenter _characterInfoPresenter;

        public void Show(IPresenter presenter)
        {
            if (presenter is not ICharacterInfoPresenter characterInfoPresenter)
            {
                throw new InvalidDataException($"{presenter} must be a {typeof(ICharacterInfoPresenter)} type");
            }
            ResetPopup();

            _characterInfoPresenter = characterInfoPresenter;
            _characterInfoPresenter.OnCharacterStatAdd += AddStatToView;
            _characterInfoPresenter.OnCharacterStatRemove += RemoveStatFromView;
            _characterInfoPresenter.AssingCharacterStats();
            _statsContentArea.AdjustScrollContentArea();

            gameObject.SetActive(true);
        }

        public void ResetPopup()
        {
            for (int i = 0; i < _characterStatsTransform.childCount; i++)
            {
                Destroy(_characterStatsTransform.GetChild(i).gameObject);
            }
            _statsDict = new Dictionary<string, StatView>();
        }

        public void AddStatToView(string name, int value, IStatPresenter statPresenter)
        {
            StatView statView = Instantiate(_statViewPrefab);
            statView.name = name;
            _statsDict[name] = statView;
            statView.transform.SetParent(_characterStatsTransform, false);
            statView.Show(statPresenter);
            _statsContentArea.AdjustScrollContentArea();
        }

        public void RemoveStatFromView(string name)
        {
            StatView statView = _statsDict[name];
            statView.transform.SetParent(null, false);
            _statsDict[name].Hide();
            Destroy(statView.gameObject);
            _statsDict.Remove(name);
            _statsContentArea.AdjustScrollContentArea();
        }

        public void Hide()
        {
            _characterInfoPresenter.OnCharacterStatAdd -= AddStatToView;
            _characterInfoPresenter.OnCharacterStatRemove -= RemoveStatFromView;
        }
    }
}