using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weapon;
        [SerializeField] private InputManager keyboardInput;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;


        private void OnEnable()
        {
            keyboardInput.OnFire += OnFlyBullet;
        }
        private void OnDisable()
        {
            keyboardInput.OnFire -= OnFlyBullet;
        }

        public void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}
