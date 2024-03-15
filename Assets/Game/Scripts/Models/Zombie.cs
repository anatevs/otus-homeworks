using UnityEngine;

public sealed partial class Zombie : MonoBehaviour,
    IFinishGameListener
{
    public PlayerEntity playerEntity;

    public AtomicVariable<Transform> targetTransform;
    public Collider targetCollider;

    public AtomicEvent<int> OnDamage = new AtomicEvent<int>();

    public AtomicVariable<bool> IsGameFinished;

    public AtomicVariable<bool> isDead;
    public AtomicVariable<bool> isDeactivated;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> hp;

    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;
    public AtomicVariable<float> rotSpeed;
    public AtomicVariable<bool> isRotationDone;

    public AtomicEvent OnDamageCounted = new AtomicEvent();
    public AtomicEvent OnResetDamageCounter = new AtomicEvent();
    public AtomicEvent AttackRequest = new AtomicEvent();
    public AtomicVariable<float> damageCounter;
    public AtomicVariable<int> damage;
    public AtomicVariable<bool> isAttacking;
    public AtomicEvent MakeDamage = new AtomicEvent();
    
    public AtomicEvent<GameObject> OnUnspawn = new AtomicEvent<GameObject>();

    private TakeDamageMechanic _takeDamageMechanic;
    private DeathMechanic _deathMechanic;
    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private RotationMechanic _rotationMechanic;
    private TowardsTargetMechanic _towardsTargetMechanic;
    private StayDuringAttackMechanic _stayDuringAttackMechanic;
    private CounterMechanic _counterMechanic_DamageToPlayer;
    private AttackCollisionMechanic _makeCollisionDamageMechanic;
    private MakeDamageMechanic2 _makeDamageMechanic;
    private UnspawnMechanic _unspawnMechanic;
    private OnFinishGameMechanic _finishGameMechanic;

    public void InitZombie(PlayerEntity playerEntity)
    {
        this.playerEntity = playerEntity;

        targetTransform.Value = this.playerEntity.GetEntityComponent<TransformComponent>().Transform;
        targetCollider = this.playerEntity.GetEntityComponent<ColliderComponent>().Collider;

        _takeDamageMechanic = new TakeDamageMechanic(OnDamage, hp);
        _deathMechanic = new DeathMechanic(isDead, hp);
        _canMoveMechanic = new CanMoveMechanic(isDead, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, moveSpeed, canMove);
        _rotationMechanic = new RotationMechanic(transform, moveDirection, rotSpeed, canMove, isRotationDone);
        _towardsTargetMechanic = new TowardsTargetMechanic(targetTransform, transform, moveDirection, IsGameFinished);
        _stayDuringAttackMechanic = new StayDuringAttackMechanic(isAttacking, canMove);
        _counterMechanic_DamageToPlayer = new CounterMechanic(OnDamageCounted, OnResetDamageCounter, damageCounter);
        _makeCollisionDamageMechanic = new AttackCollisionMechanic(OnResetDamageCounter, isAttacking, targetCollider, IsGameFinished);
        _makeDamageMechanic = new MakeDamageMechanic2(this.playerEntity, MakeDamage, damage);
        _unspawnMechanic = new UnspawnMechanic(gameObject, isDeactivated, isAttacking, OnUnspawn);
        _finishGameMechanic = new OnFinishGameMechanic(IsGameFinished);

        OnEnableSubscribtions();
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

    private void OnEnableSubscribtions()
    {
        _takeDamageMechanic.OnEnable();
        _canMoveMechanic.OnEnable();
        _rotationMechanic.OnEnable();
        _stayDuringAttackMechanic.OnEnable();
        _counterMechanic_DamageToPlayer.OnEnable();
        _makeDamageMechanic.OnEnable();
        _unspawnMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _takeDamageMechanic.OnDisable();
        _canMoveMechanic.OnDisable();
        _rotationMechanic.OnDisable();
        _stayDuringAttackMechanic.OnDisable();
        _counterMechanic_DamageToPlayer.OnDisable();
        _makeDamageMechanic.OnDisable();
        _unspawnMechanic.OnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        _makeCollisionDamageMechanic.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _makeCollisionDamageMechanic.OnTriggerExit(other);
    }

    public void OnFinishGame()
    {
        _finishGameMechanic.OnFinishGame();
    }
}