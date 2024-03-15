using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    void Update()
    {
        MakeFire();

        _playerEntity.GetEntityComponent<MoveDirectionComponent>().Direction = GetDirection();
    }

    private void MakeFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out RaycastHit hit))
            {
                Vector3 mousePos = hit.point;
                mousePos.y = 0;
                Vector3 shootDirection = (mousePos - _playerEntity.transform.position).normalized;
                _playerEntity.GetEntityComponent<InputFireEventComponent>().OnInputFire(shootDirection);
            }
        }
    }

    private Vector3 GetDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return Vector3.forward;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            return Vector3.left;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            return Vector3.back;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            return Vector3.right;
        }

        return Vector3.zero;
    }
}