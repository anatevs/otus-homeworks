using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private Bullet _bulletPrefab;

    public AtomicEvent<int> OnDamage = new AtomicEvent<int>();

    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> onDestroy;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> hp;
    
    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;
    public AtomicVariable<Vector3> rotDirection;
    public AtomicVariable<float> rotSpeed;
    public AtomicVariable<bool> isRotationDone;
    
    public AtomicEvent CanRefillWeapon = new AtomicEvent();
    public AtomicEvent OnResetWeaponCounter = new AtomicEvent();
    public AtomicVariable<float> weaponRefillTime;
    public AtomicVariable<int> bulletsStorage;
    public AtomicVariable<int> weaponRefillAmount;

    public AtomicEvent<Vector3> InputFireEvent = new AtomicEvent<Vector3>();
    public AtomicEvent<Vector3> StartFireRotation = new AtomicEvent<Vector3>();
    public AtomicEvent FireRequest = new AtomicEvent();
    public AtomicEvent ShootEvent = new AtomicEvent();
    public AtomicEvent ShootIsDone = new AtomicEvent();

    private TakeDamageMechanic _takeDamageMechanic;
    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private DestroyMechanic _destroyMechanic;
    private CounterMechanic _counterMechanic_RefillWeapon;
    private RefillWeaponMechanic _refillWeaponMechanic;
    private TryGetProjectileMechanic _tryGetProjectileMechanic;
    private ShootRotationMechanic _shootRotationMechanic;
    private ShootMechanic _shootMechanic;

    private void Awake()
    {
        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, rotDirection, rotSpeed, canMove, isRotationDone);
        _destroyMechanic = new DestroyMechanic(gameObject, onDestroy);
        _counterMechanic_RefillWeapon = new CounterMechanic(CanRefillWeapon, OnResetWeaponCounter, weaponRefillTime);
        _refillWeaponMechanic = new RefillWeaponMechanic(CanRefillWeapon, bulletsStorage, weaponRefillAmount);
        _tryGetProjectileMechanic = new TryGetProjectileMechanic(InputFireEvent, StartFireRotation, bulletsStorage);
        _shootRotationMechanic = new ShootRotationMechanic(StartFireRotation, FireRequest, ShootIsDone, isRotationDone, moveDirection, rotDirection);
        _shootMechanic = new ShootMechanic(ShootEvent, ShootIsDone, _bulletPrefab, _shootPoint, transform);
    }

    private void Update()
    {
        _deathMechanic.Update();
        _movementMechanic.Update();
        _rotationMechanic.Update();
        _counterMechanic_RefillWeapon.Update();
        _shootRotationMechanic.Update();
    }

    private void OnEnable()
    {
        _takeDamageMechanic.OnEnable();
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _refillWeaponMechanic.OnEnable();
        _rotationMechanic.OnEnable();
        _tryGetProjectileMechanic.OnEnable();
        _shootRotationMechanic.OnEnable();
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
        _shootRotationMechanic.OnDisable();
        _shootMechanic.OnDisable();
    }
}