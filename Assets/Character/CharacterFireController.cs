using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController :
        IStartGame,
        IFinishGame
    {
        private WeaponComponent _weapon;

        private IInputSystem _inputSystem;

        private BulletSystem _bulletSystem;

        private BulletConfig _bulletConfig;

        public CharacterFireController(IInputSystem inputSystem, CharacterComponents characterComponents, BulletSystem bulletSystem, BulletConfig bulletConfig)
        {
            _inputSystem = inputSystem;
            _weapon = characterComponents.WeaponComponent;
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;
        }
        public void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletArgs
            {
                isPlayer = true,
                physicsLayer = (int)_bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = _weapon.Position,
                velocity = _weapon.Rotation * Vector3.up * _bulletConfig.speed
            });
        }

        public void OnStart()
        {
            _inputSystem.OnFire += OnFlyBullet;
        }

        public void OnFinish()
        {
            _inputSystem.OnFire -= OnFlyBullet;
        }
    }
}