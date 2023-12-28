using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;
        
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IUpdate> _updateListeners = new();
        private readonly List<IPausedUpdate> _pausedUpdateListeners = new();
        private readonly List<IFixedUpdate> _fixedUpdateListeners = new();
        private readonly List<IPausedFixedUpdate> _pausedFixedUpdateListeners = new();

        private void Awake()
        {
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            AddListeners(rootGameObjects);
        }

        private void Update()
        {
            if (_gameState == GameState.Playing)
            {
                for (int i = 0; i < _updateListeners.Count; i++)
                {
                    _updateListeners[i].OnUpdate();
                }
            }
            else if (_gameState == GameState.Paused)
            {
                for (int i = 0; i < _pausedUpdateListeners.Count; i++)
                {
                    _pausedUpdateListeners[i].OnPausedUpdate();
                }
            }
        }

        private void FixedUpdate()
        {
            if (_gameState == GameState.Playing)
            {
                for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                {
                    _fixedUpdateListeners[i].OnFixedUpdate();
                }
            }
            else if (_gameState == GameState.Paused)
            {
                for (int i = 0; i < _pausedFixedUpdateListeners.Count; i++)
                {
                    _pausedFixedUpdateListeners[i].OnPausedFixedUpdate();
                }
            }
        }

        public void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is IUpdate updateListener)
            {
                _updateListeners.Add(updateListener);
            }
            if (listener is IPausedUpdate pausedUpdateListener)
            {
                _pausedUpdateListeners.Add(pausedUpdateListener);
            }
            if (listener is IFixedUpdate fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
            if (listener is IPausedFixedUpdate pausedFixedUpdateListener)
            {
                _pausedFixedUpdateListeners.Add(pausedFixedUpdateListener);
            }
        }

        public void AddListeners(IGameListener[] listeners)
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                AddListener(listeners[i]);
            }
        }

        public void AddListeners(GameObject go)
        {
            IGameListener[] listeners = go.GetComponentsInChildren<IGameListener>(true);
            AddListeners(listeners);
        }

        public void AddListeners(GameObject[] rootGameObjects)
        {
            for (int i = 0; i < rootGameObjects.Length; i++)
            {
                AddListeners(rootGameObjects[i]);
            }
        }
        public void StartGame()
        {   
            if (_gameState != GameState.NotReady)
            {
                return;
            }
            
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IStartGame startListener)
                {
                    startListener.OnStart();
                }
            }
            _gameState = GameState.Playing;
        }

        public void PauseGame()
        {
            if (_gameState != GameState.Playing)
            {
                return;
            }
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IPauseGame pauseListener)
                {
                    pauseListener.OnPause();
                }
            }
            _gameState = GameState.Paused;
        }

        public void ResumeGame()
        {
            if (_gameState != GameState.Paused)
            {
                return;
            }
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IResumeGame resumeListener)
                {
                    resumeListener.OnResume();
                }
            }
            _gameState = GameState.Playing;
        }


        public void FinishGame()
        {
            if (_gameState is GameState.Finished or GameState.NotReady)
            {
                return;
            }
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IFinishGame finishListener)
                {
                    finishListener.OnFinish();
                }
            }
            _gameState = GameState.Finished;
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}