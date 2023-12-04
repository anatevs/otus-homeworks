using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HPempty;
        
        [SerializeField] private int _hitPoints;
        
        public void TakeDamage(int damage)
        {
            this._hitPoints -= damage;
            if (this._hitPoints <= 0)
            {
                this.HPempty?.Invoke(this.gameObject);
            }
        }
    }
}