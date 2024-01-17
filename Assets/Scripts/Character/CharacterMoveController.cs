using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMoveController :
        IStartGame,
        IFinishGame
    {
        private readonly IInputSystem _inputSystem;

        private readonly MoveComponent _moveComponent;

        public CharacterMoveController(IInputSystem inputSystem, CharacterComponents characterComponents)
        {
            _inputSystem = inputSystem;
            _moveComponent = characterComponents.MoveComponent;
        }

        public void OnStart()
        {
            _inputSystem.OnMove += MoveCharacter;
        }

        public void OnFinish()
        {
            _inputSystem.OnMove -= MoveCharacter;
        }

        private void MoveCharacter(Vector2 direction)
        {
            _moveComponent.MoveByRigidbodyVelocity(direction * Time.deltaTime);
        }
    }
}