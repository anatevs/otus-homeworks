using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        MakeFire();

        _playerEntity.GetEntityComponent<MoveDirectionComponent>().Direction = GetDirection();
    }

    private void MakeFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray castPoint = _camera.ScreenPointToRay(Input.mousePosition);
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
        Vector2 inputData = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return new Vector3(inputData.x, 0, inputData.y);
    }
}