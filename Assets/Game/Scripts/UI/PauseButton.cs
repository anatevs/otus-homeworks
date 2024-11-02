using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private PauseScreen _pauseScreen;

        private void OnEnable()
        {
            _button.onClick.AddListener(_pauseScreen.Show);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_pauseScreen.Show);
        }
    }
}