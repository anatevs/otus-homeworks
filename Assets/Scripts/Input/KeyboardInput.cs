using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IPauseGame,
        GameListeners.IResumeGame,
        GameListeners.IUpdate
    {
        public bool Enabled { get; set; }

        public event Action OnFire;
        public event Action<Vector2> OnMove;

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            OnMove?.Invoke(GetDirection());
        }

        private Vector2 GetDirection()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return Vector2.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return Vector2.right;
            }            
            return Vector2.zero;
        }

        public void OnStart()
        {
            Enabled = true;
        }

        public void OnPause()
        {
            Enabled = false;
        }

        public void OnResume()
        {
            Enabled = true;
        }
    }
}