using UnityEngine;

namespace Sample
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _character;

        private MoveComponent _moveComponent;

        private void Awake()
        {
            _moveComponent = _character.GetComponent<MoveComponent>();
        }

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            Vector3 direction = this.GetDirection();
            this.Move(direction);
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction.z = -1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction.x = 1;
            }

            return direction;
        }

        private void Move(Vector3 direction)
        {
            _moveComponent.SetDirection(direction);
        }
    }
}