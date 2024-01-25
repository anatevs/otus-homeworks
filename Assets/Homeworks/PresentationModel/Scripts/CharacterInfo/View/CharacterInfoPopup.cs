using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfoPopup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _characterStatsView;

        [SerializeField]
        private GameObject _statViewPrefab;

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
            _characterInfoPresenter.OnCharacterStatChanged += FillStatViewText;

            _characterInfoPresenter.AssingCharacterStats();
            _statsContentArea.Initialize();
            _statsContentArea.AdjustScrollContentArea();

            gameObject.SetActive(true);
        }

        public void ResetPopup()
        {
            for (int i = 0; i < _characterStatsView.transform.childCount; i++)
            {
                Destroy(_characterStatsView.transform.GetChild(i).gameObject);
            }
            _statsDict = new Dictionary<string, StatView>();
        }

        public void AddStatToView(string name, int value)
        {
            GameObject statViewGameObject = Instantiate(_statViewPrefab);
            statViewGameObject.name = name;
            _statsDict[name] = statViewGameObject.GetComponent<StatView>();
            FillStatViewText(name, value);
            statViewGameObject.transform.SetParent(_characterStatsView.transform, false);
            _statsContentArea.AdjustScrollContentArea();
        }

        public void RemoveStatFromView(string name)
        {
            GameObject statViewGameObject = _statsDict[name].gameObject;
            statViewGameObject.transform.SetParent(null, false);
            Destroy(statViewGameObject);
            _statsDict.Remove(name);
            _statsContentArea.AdjustScrollContentArea();
        }

        private void FillStatViewText(string name, int value)
        {
            StatView statView = _statsDict[name];
            statView.StatText.text = $"{name}: {value}";
        }

        public void Hide()
        {
            _characterInfoPresenter.OnCharacterStatAdd -= AddStatToView;
            _characterInfoPresenter.OnCharacterStatRemove -= RemoveStatFromView;
            _characterInfoPresenter.OnCharacterStatChanged -= FillStatViewText;
        }
    }
}