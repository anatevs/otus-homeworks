using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class StartGameManager : 
        IStartable,
        ITickable,
        IStartGame
    {
        private GameManager _gameManager;

        private Button _startButton;

        private StartCountdownComponent _startCountdown;

        private int _secondsToStart;

        private int _deltaCount;

        public StartGameManager(GameManager gameManager, StartGameManagerParams startGameManagerParams)
        {
            _gameManager = gameManager;
            _startButton = startGameManagerParams.startButton;
            _startCountdown = startGameManagerParams.startCountdown;
            _secondsToStart = startGameManagerParams.secondsToStart;
            _deltaCount = startGameManagerParams.deltaCount;
        }

        void IStartable.Start()
        {
            _startButton.onClick.AddListener(StartClicked);

            if (_startCountdown != null)
            {
                _startCountdown.OnCounted += _gameManager.StartGame;
            }
        }

        void ITickable.Tick()
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

        public void OnStart()
        {
            _startCountdown.OnCounted -= _gameManager.StartGame;
        }
    }
}