using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public void Awake()
    {

    }

    public void Update()
    {

    }

    //move mechanic
    public class MovementMechanic
    {
        private readonly Transform _transform;
        private readonly AtomicVariable<Vector3> _direction;
        private readonly AtomicVariable<float> _speed;

        public MovementMechanic(Transform transform, AtomicVariable<Vector3> direction, AtomicVariable<float> speed)
        {
            _transform = transform;
            _direction = direction;
            _speed = speed;
        }

        public void Update()
        {
            _transform.Translate(_direction.Value * _speed.Value * Time.deltaTime);
        }
    }
    //rotation mechanic
    public class RotationMechanic
    {
        private readonly Transform _transform;
        private readonly AtomicVariable<Vector3> _direction;
        private readonly AtomicVariable<float> _rotSpeed;

        public void Update()
        {
            Quaternion lookQuaternion = Quaternion.LookRotation(_direction.Value);
            Quaternion.Slerp(_transform.rotation, lookQuaternion, _rotSpeed.Value * Time.deltaTime);
        }
    }

    //shooting mechanic
    public class ShootingMechanic
    {
        private readonly AtomicEvent _onShoot;
        private readonly AtomicEvent _onShootInput;

        public ShootingMechanic(AtomicEvent onShoot, AtomicEvent onShootInput)
        {
            _onShoot = onShoot;
            _onShootInput = onShootInput;
        }

        public void OnEnable()
        {
            _onShootInput.Subscribe(MakeShoot);
        }

        public void OnDisable()
        {
            _onShoot.Unsubscribe(MakeShoot);
        }

        private void MakeShoot()
        {
            _onShoot.Invoke();
        }
    }

    //bullets refill mechanic
    public class RefillWeaponMechanic
    {
        private readonly AtomicEvent<bool> _canRefill;
        private readonly AtomicVariable<int> _weaponMagazine;
        private readonly AtomicVariable<int> _refillAmout;

        public RefillWeaponMechanic(AtomicEvent<bool> canRefill, AtomicVariable<int> weaponMagazine, AtomicVariable<int> refillAmout)
        {
            _canRefill = canRefill;
            _weaponMagazine = weaponMagazine;
            _refillAmout = refillAmout;
        }

        public void OnEnable()
        {
            _canRefill.Subscribe(MakeWeaponRefill);
        }

        public void OnDesable()
        {
            _canRefill.Unsubscribe(MakeWeaponRefill);
        }

        private void MakeWeaponRefill(bool canRefill)
        {
            _weaponMagazine.Value += _refillAmout.Value;
        }
    }

    //take damage mechanic
    public class TakeDamageMechanic
    {
        private readonly AtomicEvent<int> _onTakeDamage;
        private readonly AtomicVariable<int> _hp;

        public TakeDamageMechanic(AtomicEvent<int> onTakeDamage, AtomicVariable<int> hp)
        {
            _onTakeDamage = onTakeDamage;
            _hp = hp;
        }

        public void OnEnable()
        {
            _onTakeDamage.Subscribe(MakeDamage);
        }

        public void OnDesable()
        {
            _onTakeDamage.Unsubscribe(MakeDamage);
        }

        private void MakeDamage(int damage)
        {
            _hp.Value -= damage;
            _hp.Value = Mathf.Max(0, _hp.Value);
        }
    }

    //death mechanic
    public class DeathMechanic
    {
        private readonly AtomicEvent _onDeath;
        private readonly AtomicVariable<int> _hp;
        private readonly GameObject _gameObject;

        public DeathMechanic(AtomicEvent onDeath, AtomicVariable<int> hp, GameObject gameObject)
        {
            _onDeath = onDeath;
            _hp = hp;
            _gameObject = gameObject;
        }

        public void Update()
        {
            if (_hp.Value > 0)
            {
                return;
            }
            else
            {
                _onDeath?.Invoke();
                Object.Destroy(_gameObject);
            }
        }
    }
}