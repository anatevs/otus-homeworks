using ShootEmUp;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour,
    GameListeners.IStartGame,
    GameListeners.IFinishGame
{
    [SerializeField]
    private KeyboardInput _keyboardInput;

    [SerializeField]
    private GameObject _character;

    public void OnStart()
    {
        this._keyboardInput.OnMove += MoveCharacter;
    }

    public void OnFinish()
    {
        this._keyboardInput.OnMove -= MoveCharacter;
    }

    void MoveCharacter(Vector2 direction)
    {
        this._character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(direction * Time.deltaTime);
    }
}
