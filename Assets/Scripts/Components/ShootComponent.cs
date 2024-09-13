using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Sample
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField]
        private float _reloadTime = 2f;
        
        [SerializeField]
        private bool _isReloading;

        [SerializeField]
        private GameObject _bulletPrefab;
        
        [SerializeField]
        private Transform _firePoint;

        [ShowInInspector, ReadOnly]
        private float _reloadTimer;

        private readonly AndCondition _condition = new();

        private void Update()
        {
            if (_isReloading)
            {
                _reloadTimer -= Time.deltaTime;

                if (_reloadTimer <= 0)
                {
                    _isReloading = false;
                }
            }
        }
        
        public void Shoot()
        {
            if (_isReloading)
            {
                return;
            }

            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            _reloadTimer = _reloadTime;
            _isReloading = true;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}