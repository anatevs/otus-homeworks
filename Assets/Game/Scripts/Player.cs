using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    public AtomicEvent FireEvent = new AtomicEvent();
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
        _movementMechanic = new MovementMechanic(gameObject.transform, moveDirection, moveSpeed, canMove);
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
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _refillWeaponMechanic.OnEnable();
        _tryGetProjectileMechanic.OnEnable();
        _shootMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _refillWeaponMechanic.OnDisable();
        _tryGetProjectileMechanic.OnDisable();
        _shootMechanic.OnDisable();
    }
}