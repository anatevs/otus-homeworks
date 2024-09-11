using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Chest
{
    public class Chest : MonoBehaviour
    {
        public event Action OnOpened;
        public event Action OnClosed;

        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                _isOpened = value;
                _animator.SetBool("IsOpened", _isOpened);
            }
        }

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private bool _isOpened;

        private bool _temp;

        private void Awake()
        {
            _temp = _isOpened;
        }

        private void Update()
        {
            if (_isOpened != _temp)
            {
                IsOpened = _isOpened;
                _temp = _isOpened;
            }
        }


        public void Open()
        {
            IsOpened = true;
        }

        public void Close()
        {
            IsOpened = false;
        }

        public void OpenAnimEnd()
        {
            Debug.Log("opened");
            OnOpened?.Invoke();
        }

        public void CloseAnimEnd()
        {
            Debug.Log("closed");
            OnClosed?.Invoke();
        }
    }
}