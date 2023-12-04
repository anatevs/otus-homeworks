using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class KeyboardInput : MonoBehaviour
    {
        public event Action OnFire;
        public event Action<Vector2> OnMove;

        private void Update()
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
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return Vector2.right;
            }            
            return Vector2.zero;
        }

    }
}