using System;
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