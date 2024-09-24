using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Chest
{
    [RequireComponent(typeof(Button))]
    public class ChestButton : MonoBehaviour
    {
        public event Action OnClicked;

        [SerializeField]
        private ChestUIAnim _buttonAnimation;

        [SerializeField]
        private TMP_Text _counterText;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            MakeDisabled();

            _button.onClick.AddListener(_buttonAnimation.Open);
            _button.onClick.AddListener(DoOnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void MakeInteractable()
        {
            ChangeInteractable(true);
        }

        public void UpdateCounterText(TimeSpan remainder)
        {
            _counterText.text = remainder.ToString(@"hh\hmm\mss\s");
        }

        private void DoOnClick()
        {
            MakeDisabled();
            OnClicked?.Invoke();
        }

        private void MakeDisabled()
        {
            ChangeInteractable(false);
        }

        private void ChangeInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}