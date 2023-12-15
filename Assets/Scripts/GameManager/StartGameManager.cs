using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{

    public class StartGameManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private StartCountdownComponent _startCountdown;

        [SerializeField]
        private int _secondsToStart = 3;

        [SerializeField]
        private int _deltaCount = 1;

        private void Start()
        {
            _startButton.onClick.AddListener(StartClicked);

            if (_startCountdown != null)
            {
                _startCountdown.OnCounted += _gameManager.StartListeners;
            }
        }

        private void Update()
        {
            if (_startCountdown.isActiveAndEnabled)
            {
                _startCountdown.CountTime(Time.time, _deltaCount);
            }
        }

        private void StartClicked()
        {
            _startCountdown.gameObject.SetActive(true);
            _startCountdown.InitaliseCounter(_secondsToStart);
            _startButton.gameObject.SetActive(false);
        }
    }
}