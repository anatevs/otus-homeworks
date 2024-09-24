using System;
using UnityEngine;

namespace Scripts.Chest
{
    [RequireComponent(typeof(Animator))]
    public class ChestUIAnim : MonoBehaviour
    {
        public event Action OnOpened;

        private Animator _animator;

        private const string PRESS_TRIGGER = "Pressed";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Open()
        {
            _animator.SetTrigger(PRESS_TRIGGER);
        }

        public void DoOnOpened()
        {
            Debug.Log("opened");
            OnOpened?.Invoke();
        }
    }
}