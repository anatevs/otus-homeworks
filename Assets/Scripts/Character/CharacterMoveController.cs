using ShootEmUp;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour,
    IStartGame,
    IFinishGame
{
    [SerializeField]
    private KeyboardInput _keyboardInput;

    [SerializeField]
    private GameObject _character;

    public void OnStart()
    {
        _keyboardInput.OnMove += MoveCharacter;
    }

    public void OnFinish()
    {
        _keyboardInput.OnMove -= MoveCharacter;
    }

    void MoveCharacter(Vector2 direction)
    {
        _character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(direction * Time.deltaTime);
    }
}
