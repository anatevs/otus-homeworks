using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController :
        IStartGame,
        IFinishGame
    {
        private readonly WeaponComponent _weapon;

        private readonly IInputSystem _inputSystem;

        private readonly BulletSystem _bulletSystem;

        private readonly BulletConfig _bulletConfig;

        public CharacterFireController(IInputSystem inputSystem, CharacterComponents characterComponents, BulletSystem bulletSystem, BulletConfig bulletConfig)
        {
            _inputSystem = inputSystem;
            _weapon = characterComponents.WeaponComponent;
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletArgs
            (
                _weapon.Position,
                _weapon.Rotation * Vector3.up * _bulletConfig.speed,
                _bulletConfig.color,
                (int)_bulletConfig.physicsLayer,
                _bulletConfig.damage,
                true
            ));
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