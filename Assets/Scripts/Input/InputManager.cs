using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public Action OnFire;
        public event Action<Vector2, float> OnMove;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            float deltaTime = Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector2.left, deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector2.right, deltaTime);
            }
            else
            {
                Move(Vector2.zero, deltaTime);
            }
        }

        void Move(Vector2 direction, float deltaTime)
        {
            OnMove?.Invoke(direction, deltaTime);
        }
    }
}