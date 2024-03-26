using UnityEngine;

public sealed partial class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private AnimationDispatcher _animDispatcher;

    private readonly PlayerAnimEventsNames _animEventsNames;

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
    public AtomicVariable<int> bulletStorage;
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
    private AnimEventMechanic_OnShoot _onShootAnimEventMechanic;
    private AnimEventMechanic_OnDestroy _onDestoyAnimEventMechanic;

    private void Awake()
    {
        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, rotDirection, rotSpeed, canMove, isRotationDone);
        _destroyMechanic = new DestroyMechanic(gameObject, onDestroy);
        _counterMechanic_RefillWeapon = new CounterMechanic(CanRefillWeapon, OnResetWeaponCounter, weaponRefillTime);
        _refillWeaponMechanic = new RefillWeaponMechanic(CanRefillWeapon, bulletStorage, weaponRefillAmount);
        _tryGetProjectileMechanic = new TryGetProjectileMechanic(InputFireEvent, StartFireRotation, bulletStorage);
        _shootRotationMechanic = new ShootRotationMechanic(StartFireRotation, FireRequest, ShootIsDone, isRotationDone, moveDirection, rotDirection);
        _shootMechanic = new ShootMechanic(ShootEvent, ShootIsDone, _bulletPrefab, _shootPoint, transform);
        _onShootAnimEventMechanic = new AnimEventMechanic_OnShoot(ShootEvent, _animDispatcher, _animEventsNames);
        _onDestoyAnimEventMechanic = new AnimEventMechanic_OnDestroy(onDestroy, _animDispatcher, _animEventsNames);
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
        _onShootAnimEventMechanic.OnEnable();
        _onDestoyAnimEventMechanic.OnEnable();
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
        _onShootAnimEventMechanic.OnDisable();
        _onDestoyAnimEventMechanic.OnDisable();
    }
}