using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ShootEmUp
{
    public class StartCountdownComponent : MonoBehaviour,
        GameListeners.IStartGame
    {
        public event Action OnCounted;

        private Text _counterText;
        private int _count;
        private float _countTime;

        public void OnStart()
        {
            UnsubscribeFromAll(OnCounted);
            gameObject.SetActive(false);
        }

        public void InitaliseCounter(int secondsToStart)
        {
            _counterText = GetComponent<Text>();
            _countTime = Time.time;
            _count = secondsToStart;
            _counterText.text = _count.ToString();
        }

        public void CountTime(float currTime, int deltaCount)
        {
            if (currTime - _countTime >= deltaCount)
            {
                _countTime = currTime;
                _count -= deltaCount;
                if (_count > 0) { _counterText.text = _count.ToString(); }

                if (_count <= 0)
                {
                    OnCounted?.Invoke();
                }
            }
        }

        public void UnsubscribeFromAll(Action action)
        {
            Delegate[] listeners = action.GetInvocationList();
            if (listeners != null && listeners.Length != 0)
            {
                for (int i = 0; i < listeners.Length; i++)
                {
                    action -= (listeners[i] as Action);
                }
            }
        }
    }
}