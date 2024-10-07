using UnityEngine;

namespace Scripts.Chest
{
    public sealed class ChestController : MonoBehaviour
    {
        [SerializeField]
        private ChestButton _chestButton;

        [SerializeField]
        private ChestTimer _chestTimer;

        [SerializeField]
        private Chest _chest;

        private void OnEnable()
        {
            _chestTimer.OnCounted += _chestButton.MakeInteractable;

            _chestButton.OnClicked += _chestTimer.ResetCounter;
            _chestButton.OnClicked += _chest.GoToNextReward;
        }

        private void OnDisable()
        {
            _chestTimer.OnCounted -= _chestButton.MakeInteractable;

            _chestButton.OnClicked -= _chestTimer.ResetCounter;
            _chestButton.OnClicked -= _chest.GoToNextReward;
        }

        private void Update()
        {
            _chestButton.UpdateCounterText(_chestTimer.RemainderSpan);
        }
    }
}