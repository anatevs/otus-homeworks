using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameManagerData
    {
        private readonly List<IGameListener> _gameListeners = new();

        private readonly List<IUpdate> _updateListeners = new();

        private readonly List<IPausedUpdate> _pausedUpdateListeners = new();

        private readonly List<IFixedUpdate> _fixedUpdateListeners = new();

        private readonly List<IPausedFixedUpdate> _pausedFixedUpdateListeners = new();

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

        public void AddListeners(IEnumerable<IGameListener> listeners)
        {
            foreach(IGameListener listener in listeners)
            {
                AddListener((IGameListener)listener);
            }
            //for (int i = 0; i < listeners.Count; i++)
            //{
            //    AddListener(listeners[i]);
            //}
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

        public void StartListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IStartGame startListener)
                {
                    startListener.OnStart();
                }
            }
        }

        public void PauseListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IPauseGame pauseListener)
                {
                    pauseListener.OnPause();
                }
            }
        }

        public void ResumeListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IResumeGame resumeListener)
                {
                    resumeListener.OnResume();
                }
            }
        }

        public void FinishListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
            {
                if (_gameListeners[i] is IFinishGame finishListener)
                {
                    finishListener.OnFinish();
                }
            }
        }

        public void UpdateListeners()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate();
            }
        }
        public void PausedUpdateListeners()
        {
            for (int i = 0; i < _pausedUpdateListeners.Count; i++)
            {
                _pausedUpdateListeners[i].OnPausedUpdate();
            }
        }

        public void FixedUpdateListeners()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate();
            }
        }

        public void PausedFixedUpdateListeners()
        {
            for (int i = 0; i < _pausedFixedUpdateListeners.Count; i++)
            {
                _pausedFixedUpdateListeners[i].OnPausedFixedUpdate();
            }
        }
    }
}