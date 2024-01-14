using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInputSystem
    {
        public event Action OnFire;

        public event Action<Vector2> OnMove;
    }
}