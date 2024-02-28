using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private Bullet _bulletPrefab;

    public AtomicEvent<int> OnDamage = new AtomicEvent<int>();

    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> hp;
    
    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;
    public AtomicVariable<Vector3> rotDirection;
    public AtomicVariable<float> rotSpeed;
    
    public AtomicEvent CanRefillWeapon = new AtomicEvent();
    public AtomicVariable<float> weaponRefillTime;
    public AtomicVariable<int> bulletsStorage;
    public AtomicVariable<int> weaponRefillAmount;

    public AtomicEvent<Vector3> FireEvent = new AtomicEvent<Vector3>();
    public AtomicVariable<bool> _canShoot;
    public AtomicEvent ShootEvent = new AtomicEvent();

    private TakeDamageMechanic _takeDamageMechanic;
    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private DestroyMechanic _destroyMechanic;
    private CounterMechanic _counterMechanic_RefillWeapon;
    private RefillWeaponMechanic _refillWeaponMechanic;
    private TryGetProjectileMechanic _tryGetProjectileMechanic;
    private ShootMechanic _shootMechanic;

    private void Awake()
    {
        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, rotDirection, rotSpeed, canMove);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _counterMechanic_RefillWeapon = new CounterMechanic(CanRefillWeapon, weaponRefillTime);
        _refillWeaponMechanic = new RefillWeaponMechanic(CanRefillWeapon, bulletsStorage, weaponRefillAmount);
        _tryGetProjectileMechanic = new TryGetProjectileMechanic(FireEvent, ShootEvent, bulletsStorage);
        _shootMechanic = new ShootMechanic(ShootEvent, _bulletPrefab, _shootPoint, transform);
    }

    private void Update()
    {
        _deathMechanic.Update();
        _movementMechanic.Update();
        _rotationMechanic.Update();
        _counterMechanic_RefillWeapon.Update();
    }

    private void OnEnable()
    {
        _takeDamageMechanic.OnEnable();
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _refillWeaponMechanic.OnEnable();
        _rotationMechanic.OnEnable();
        _tryGetProjectileMechanic.OnEnable();
        _shootMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _takeDamageMechanic.OnDisable();
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _refillWeaponMechanic.OnDisable();
        _rotationMechanic.OnDisable();
        _tryGetProjectileMechanic.OnDisable();
        _shootMechanic.OnDisable();
    }

    public class RotationDirectionMechanic
    {
        private IAtomicEvent<Vector3> _onFire;
        private IAtomicAction _shootAction;
        private IAtomicValue<Vector3> _moveDirection;
        private IAtomicVariable<Vector3> _rotDirection;
        private IAtomicVariable<bool> _isShooting;
        private IAtomicValue<bool> _isRotateDone;

        private Vector3 _shootDirection;

        public void OnEnable()
        {
            _onFire.Subscribe(StartFireProcess);
        }

        public void OnDisable()
        {
            _onFire.Unsubscribe(StartFireProcess);
        }

        public void Update()
        {
            if (!_isShooting.Value) 
            {
                _rotDirection.Value = _moveDirection.Value;
            }
            else
            {
                _rotDirection.Value = _shootDirection;
                if (!_isRotateDone.Value)
                {
                    return;
                }
                else
                {
                    _shootAction.Invoke();
                }
            }
        }

        private void StartFireProcess(Vector3 shootDirection)
        {
            _isShooting.Value = true;
            _shootDirection = shootDirection;
        }
    }
}