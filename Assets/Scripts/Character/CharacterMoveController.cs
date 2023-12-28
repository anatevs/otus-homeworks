using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveController :
        IStartGame,
        IFinishGame
    {
        private IInputSystem _inputSystem;
        private MoveComponent _moveComponent;

        public CharacterMoveController(IInputSystem inputSystem, CharacterComponents characterComponents)
        {
            _inputSystem = inputSystem;
            _moveComponent = characterComponents.GetComponent<MoveComponent>();
        }

        public void OnStart()
        {
            _inputSystem.OnMove += MoveCharacter;
        }

        public void OnFinish()
        {
            _inputSystem.OnMove -= MoveCharacter;
        }

        void MoveCharacter(Vector2 direction)
        {
            _moveComponent.MoveByRigidbodyVelocity(direction * Time.deltaTime);
        }
    }
}