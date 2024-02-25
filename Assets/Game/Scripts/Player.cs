using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public partial class Player : MonoBehaviour
{
    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> hp;
    
    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;
    public AtomicVariable<Vector3> rotDirection;
    public AtomicVariable<float> rotSpeed;
    
    public AtomicEvent canRefillWeapon;
    public AtomicVariable<float> weaponRefillTime;
    public AtomicVariable<int> weaponMagazine;
    public AtomicVariable<int> weaponRefillAmount;


    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private DestroyMechanic _destroyMechanic;
    private CounterMechanic _counterMechanic_RefillWeapon;
    private RefillWeaponMechanic _refillWeaponMechanic;

    public void Awake()
    {
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(gameObject.transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, rotDirection, rotSpeed, canMove);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _counterMechanic_RefillWeapon = new CounterMechanic(canRefillWeapon, weaponRefillTime);
        _refillWeaponMechanic = new RefillWeaponMechanic(canRefillWeapon, weaponMagazine, weaponRefillAmount);
    }

    public void Update()
    {
        _deathMechanic.Update();
        _movementMechanic.Update();
        _rotationMechanic.Update();
        _counterMechanic_RefillWeapon.Update();

    }

    public void OnEnable()
    {
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _refillWeaponMechanic.OnEnable();
    }

    public void OnDisable()
    {
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _refillWeaponMechanic.OnDisable();
    }
}