using UnityEngine;

namespace Scripts.Chest
{
    public class Chest : MonoBehaviour
    {
        [SerializeField]
        private ChestButton _chestButton;

        [SerializeField]
        private ChestTimer _chestTimer;

        private void Awake()
        {
            _chestButton.OnClicked += _chestTimer.ResetCounter;

            _chestTimer.OnCounted += _chestButton.MakeInteractable;
        }

        private void OnDisable()
        {
            _chestButton.OnClicked -= _chestTimer.ResetCounter;

            _chestTimer.OnCounted -= _chestButton.MakeInteractable;
        }

        private void Update()
        {
            _chestButton.UpdateCounterText(_chestTimer.RemainderSpan);
        }
    }
}