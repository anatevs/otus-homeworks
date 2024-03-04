using UnityEngine;

public partial class Zombie : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    public AtomicEvent<int> OnDamage = new AtomicEvent<int>();

    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> hp;

    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;
    public AtomicVariable<float> rotSpeed;
    public AtomicVariable<bool> isRotationDone;

    public AtomicEvent OnDamageCounted = new AtomicEvent();
    public AtomicEvent OnResetDamageCounter = new AtomicEvent();
    public AtomicVariable<float> damageCounter;
    public AtomicVariable<int> damage;

    private TakeDamageMechanic _takeDamageMechanic;
    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private DestroyMechanic _destroyMechanic;
    private TowardsTargetMechanic _towardsTargetMechanic;
    private CounterMechanic _counterMechanic_DamageToPlayer;
    private MakeCollisionDamageMechanic _makeCollisionDamageMechanic;

    private void Awake()
    {
        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, moveDirection, rotSpeed, canMove, isRotationDone);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _towardsTargetMechanic = new TowardsTargetMechanic(_playerTransform, transform, moveDirection);
        _counterMechanic_DamageToPlayer = new CounterMechanic(OnDamageCounted, OnResetDamageCounter, damageCounter);
        _makeCollisionDamageMechanic = new MakeCollisionDamageMechanic(OnDamageCounted, OnResetDamageCounter, damageCounter, damage);
    }

    private void Update()
    {
        _deathMechanic.Update();
        _movementMechanic.Update();
        _rotationMechanic.Update();
        _towardsTargetMechanic.Update();
        _counterMechanic_DamageToPlayer.Update();
        _makeCollisionDamageMechanic.Update();
    }

    private void OnEnable()
    {
        _takeDamageMechanic.OnEnable();
        _canMoveMechanic.OnEnable();
        _rotationMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _counterMechanic_DamageToPlayer.OnEnable();
        _makeCollisionDamageMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _takeDamageMechanic.OnDisable();
        _canMoveMechanic.OnDisable();
        _rotationMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _counterMechanic_DamageToPlayer.OnDisable();
        _makeCollisionDamageMechanic.OnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        _makeCollisionDamageMechanic.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _makeCollisionDamageMechanic.OnTriggerExit(other);
    }
}