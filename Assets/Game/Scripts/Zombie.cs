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


    private TakeDamageMechanic _takeDamageMechanic;
    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private DestroyMechanic _destroyMechanic;
    private TowardsTargetMechanic _towardsTargetMechanic;

    private void Awake()
    {
        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, moveDirection, rotSpeed, canMove, isRotationDone);
        _destroyMechanic = new DestroyMechanic(gameObject, isDead);
        _towardsTargetMechanic = new TowardsTargetMechanic(_playerTransform, transform, moveDirection);
    }

    private void Update()
    {
        _deathMechanic.Update();
        _movementMechanic.Update();
        _rotationMechanic.Update();
        _towardsTargetMechanic.Update();
    }

    private void OnEnable()
    {
        _takeDamageMechanic.OnEnable();
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _takeDamageMechanic.OnDisable();
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
    }
}