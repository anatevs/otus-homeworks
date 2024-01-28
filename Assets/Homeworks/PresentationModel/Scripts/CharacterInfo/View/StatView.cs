using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class StatView : MonoBehaviour
    {
        public Text StatText => _text;

        [SerializeField]
        private Text _text;

        private IStatPresenter _statPresenter;

        public void Show(IPresenter presenter)
        {
            if (presenter is not IStatPresenter statPresenter)
            {
                throw new Exception($"the {presenter} is not a {typeof(IStatPresenter)} type");
            }

            _statPresenter = statPresenter;
            _statPresenter.OnCharacterStatChanged += FillStatText;
            FillStatText();
        }

        private void FillStatText()
        {
            _text.text = _statPresenter.StatText;
        }

        public void Hide()
        {
            _statPresenter.OnCharacterStatChanged -= FillStatText;
        }
    }
}