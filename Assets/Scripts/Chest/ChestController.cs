using TMPro;
using UnityEngine;

namespace Scripts.Chest
{
    [RequireComponent(typeof(Chest), typeof(ChestTimer))]
    public sealed class ChestController : MonoBehaviour
    {
        [SerializeField]
        private ChestButton _chestButton;

        [SerializeField]
        private TMP_Text _nextRewardText;

        private ChestTimer _chestTimer;

        private Chest _chest;

        private void Awake()
        {
            _chest = GetComponent<Chest>();
            _chestTimer = GetComponent<ChestTimer>();
        }

        private void OnEnable()
        {
            SetNextRewardText();

            _chestTimer.OnCounted += _chestButton.MakeInteractable;

            _chestButton.OnClicked += _chestTimer.ResetCounter;
            _chestButton.OnClicked += _chest.GoToNextReward;
            _chestButton.OnClicked += SetNextRewardText;
        }

        private void OnDisable()
        {
            _chestTimer.OnCounted -= _chestButton.MakeInteractable;

            _chestButton.OnClicked -= _chestTimer.ResetCounter;
            _chestButton.OnClicked -= _chest.GoToNextReward;
            _chestButton.OnClicked -= SetNextRewardText;
        }

        private void Update()
        {
            _chestButton.UpdateCounterText(_chestTimer.RemainderSpan);
        }

        private void SetNextRewardText()
        {
            var reward = _chest.Reward;

            _nextRewardText.text = $"Next reward:\n{reward.Value} {reward.Currency}";
        }
    }
}