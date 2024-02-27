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

    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private DestroyMechanic _destroyMechanic;
    private CollisionMechanic _collisionMechanic;

    public void Awake()
    {
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, speed, canMove);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _collisionMechanic = new CollisionMechanic(damage, isDead);
    }

    public void Update()
    {
        _movementMechanic.Update();
    }

    public void OnEnable()
    {
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
    }

    public void OnDisable()
    {
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
    }

    public void OnTriggerEnter(Collider other)
    {
        _collisionMechanic.OnTriggerEnter(other);
    }
}