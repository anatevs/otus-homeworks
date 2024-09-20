using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Chest
{
    [RequireComponent(typeof(Button))]
    public class ChestButton : MonoBehaviour
    {
        [SerializeField]
        private ChestUIAnim _spriteAnimation;

        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            
            MakeDisabled();

            _button.onClick.AddListener(_spriteAnimation.Open);
            _button.onClick.AddListener(MakeDisabled);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void MakeInteractivable()
        {
            _button.interactable = true;
        }

        private void MakeDisabled()
        {
            _button.interactable = false;
        }
    }
}