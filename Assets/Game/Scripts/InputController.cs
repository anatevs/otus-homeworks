using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.OnShoot?.Invoke();
        }
    }
}
