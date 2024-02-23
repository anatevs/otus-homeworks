using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public partial class Player : MonoBehaviour
{
    public AtomicVariable<Vector3> moveDirection;
    public AtomicVariable<float> moveSpeed;

    private MovementMechanic _movementMechanic;

    public void Awake()
    {
        _movementMechanic = new MovementMechanic(gameObject.transform, moveDirection, moveSpeed);
    }

    public void Update()
    {
        _movementMechanic.Update();
    }
}