using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;


        private void OnEnable()
        {
            this._keyboardInput.OnFire += OnFlyBullet;
        }
        private void OnDisable()
        {
            this._keyboardInput.OnFire -= OnFlyBullet;
        }

        public void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int)this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = this._weapon.Position,
                velocity = this._weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}
