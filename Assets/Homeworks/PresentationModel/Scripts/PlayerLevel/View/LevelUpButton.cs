using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class LevelUpButton : MonoBehaviour
    {
        [SerializeField]
        private Button _levelUpButton;

        [SerializeField]
        private Sprite _activeStateImage;

        [SerializeField]
        private Sprite _inactiveStateImage;

        public void AddListener(UnityAction action)
        {
            _levelUpButton.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            _levelUpButton.onClick.RemoveListener(action);
        }

        public void SetButtonToActive()
        {
            _levelUpButton.interactable = true;
            _levelUpButton.image.sprite = _activeStateImage;
        }

        public void SetButtonToInactive()
        {
            _levelUpButton.interactable = false;
            _levelUpButton.image.sprite = _inactiveStateImage;
        }
    }
}