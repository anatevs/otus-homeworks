using UnityEngine;

public partial class Bullet : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> damage;

    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> speed;

    public AtomicEvent OnShoot = new AtomicEvent();

    public AtomicEvent OnLifetimeEnd = new AtomicEvent();
    public AtomicEvent OnLifetimeReset = new AtomicEvent();
    public AtomicVariable<float> _lifetime;

    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private DestroyMechanic _destroyMechanic;
    private CollisionMechanic _collisionMechanic;
    private CounterMechanic _counterMechanic_Lifetime;
    private LifetimeMechanic _lifetimeMechanic;

    private void Awake()
    {
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, speed, canMove);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _collisionMechanic = new CollisionMechanic(damage, isDead);
        _counterMechanic_Lifetime = new CounterMechanic(OnLifetimeEnd, OnLifetimeReset, _lifetime);
        _lifetimeMechanic = new LifetimeMechanic(OnLifetimeEnd, isDead);
    }

    private void Update()
    {
        _movementMechanic.Update();
        _counterMechanic_Lifetime.Update();
    }

    private void OnEnable()
    {
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _lifetimeMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _lifetimeMechanic.OnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionMechanic.OnTriggerEnter(other);
    }
}