using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.FireEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.moveDirection.Value = Vector3.forward;
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.moveDirection.Value = Vector3.left;
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _player.moveDirection.Value = Vector3.back;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _player.moveDirection.Value = Vector3.right;
            return;
        }
    }
}