using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;

        private List<GameListeners.IGameListener> _gameListeners = new();
        private List<GameListeners.IUpdate> _updateListeners = new();
        private List<GameListeners.IFixedUpdate> _fixedUpdateListeners = new();

        private void Start()
        {
            _pauseButton.onClick.AddListener(PauseListeners);
            _resumeButton.onClick.AddListener(ResumeListeners);
        }

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                if (_updateListeners[i].Enabled)
                {
                    _updateListeners[i].OnUpdate();
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                if (_fixedUpdateListeners[i].Enabled)
                {
                    _fixedUpdateListeners[i].OnFixedUpdate();
                }
            }
        }

        public void AddGameListener(GameListeners.IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is GameListeners.IUpdate updateListener)
            {
                _updateListeners.Add(updateListener);
            }
            if (listener is GameListeners.IFixedUpdate fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
        }

        public void RemoveGameListener(GameListeners.IGameListener listener)
        {
            _gameListeners.Remove(listener);

            if (listener is GameListeners.IUpdate updateListener)
            {
                _updateListeners.Remove(updateListener);
            }
            if (listener is GameListeners.IFixedUpdate fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }
        }

        public void StartListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is GameListeners.IStartGame startListener)
                {
                    startListener.OnStart();
                }
            }
        }

        public void PauseListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is GameListeners.IPauseGame pauseListener)
                {
                    pauseListener.OnPause();
                }
            }
        }

        public void ResumeListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
                if (_gameListeners[i] is GameListeners.IResumeGame resumeListener)
                {
                    resumeListener.OnResume();
                }
        }


        public void FinishListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is GameListeners.IFinishGame finishListener)
                {
                    finishListener.OnFinish();
                }
            }
            
        }

        public void FinishGame()
        {
            FinishListeners();
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

    }
}