using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [SerializeField]
    InputManager keyboardInput;

    [SerializeField]
    private GameObject character;


    private void OnEnable()
    {
        keyboardInput.OnMove += MoveCharacter;
    }

    private void OnDisable()
    {
        keyboardInput.OnMove -= MoveCharacter;
    }

    void MoveCharacter(Vector2 direction, float deltaTime)
    {
        this.character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(direction * deltaTime);
    }
}
