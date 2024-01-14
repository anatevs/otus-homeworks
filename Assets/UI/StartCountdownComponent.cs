using System;
using UnityEngine;
using UnityEngine.UI;


namespace ShootEmUp
{
    public sealed class StartCountdownComponent : MonoBehaviour,
        IStartGame
    {
        public event Action OnCounted;

        private Text _counterText;

        private int _count;

        private float _countTime;

        public void OnStart()
        {
            OnCounted = null;
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
    }
}