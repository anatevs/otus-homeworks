using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour,
        IStartGame,
        IFinishGame
    {
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        public void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.BulletArgs
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
            _keyboardInput.OnFire += OnFlyBullet;
        }

        public void OnFinish()
        {
            _keyboardInput.OnFire -= OnFlyBullet;
        }
    }
}
