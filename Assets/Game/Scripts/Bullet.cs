using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Bullet : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _shootPoint;

    public AtomicVariable<bool> isCollided;
    public AtomicVariable<bool> canMove;
    public AtomicVariable<int> damage;

    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> speed;

    public AtomicEvent OnShoot = new AtomicEvent();

    private CanMoveMechanic _canMoveMechanic;
    private MovementMechanic _movementMechanic;
    private DestroyMechanic _destroyMechanic;
    private SpawnMechanic _spawnMechanic;

    public void Awake()
    {
        _canMoveMechanic = new CanMoveMechanic(isCollided, canMove);
        _movementMechanic = new MovementMechanic(transform, moveDirection, speed, canMove);
        _destroyMechanic = new DestroyMechanic(gameObject, isCollided);
        _spawnMechanic = new SpawnMechanic(_player.OnShoot, gameObject, _shootPoint.position, _player.transform.rotation);
    }

    public void Update()
    {

        _movementMechanic.Update();

    }

    public void OnEnable()
    {
        _canMoveMechanic.OnEnable();
        _destroyMechanic.OnEnable();
        _spawnMechanic.OnEnable();
    }

    public void OnDisable()
    {
        _canMoveMechanic.OnDisable();
        _destroyMechanic.OnDisable();
        _spawnMechanic.OnDisable();
    }
}