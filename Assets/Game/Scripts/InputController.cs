using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(castPoint, out RaycastHit hit))
            {
                Vector3 mousePos = hit.point;
                Vector3 shootDirection = (mousePos - _player.transform.position).normalized;
                _player.FireEvent?.Invoke(shootDirection);
            }
        }

        _player.moveDirection.Value = GetDirection();
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