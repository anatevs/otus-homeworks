using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : 
        IInputSystem,
        IUpdate,
        IPausedUpdate
    {
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

        public void OnPausedUpdate()
        {
            return;
        }

        private static Vector2 GetDirection()
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
    }
}