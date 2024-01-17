using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<HitPointsComponent> OnHPZero;
        
        [SerializeField] private int _hitPoints;
        
        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHPZero?.Invoke(this);
            }
        }
    }
}