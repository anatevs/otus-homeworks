using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM {
    public sealed class CharacterPopup : MonoBehaviour
    {
        public event Action OnHidePopup;

        [SerializeField]
        private GameObject _characterPopup;

        [SerializeField]
        private Button _closeButton;

        public void Show()
        {
            _characterPopup.SetActive(true);
            _closeButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            OnHidePopup?.Invoke();
            _closeButton.onClick.RemoveListener(Hide);
            _characterPopup.SetActive(false);
        }
    }
}