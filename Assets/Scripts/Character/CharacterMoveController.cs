using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [SerializeField]
    private KeyboardInput _keyboardInput;

    [SerializeField]
    private GameObject _character;


    private void OnEnable()
    {
        this._keyboardInput.OnMove += MoveCharacter;
    }

    private void OnDisable()
    {
        this._keyboardInput.OnMove -= MoveCharacter;
    }

    void MoveCharacter(Vector2 direction)
    {
        this._character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(direction * Time.deltaTime);
    }
}
