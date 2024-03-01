using UnityEngine;

public partial class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    private CameraFollowMechanic _cameraFollowMechanic;

    private void Awake()
    {
        _cameraFollowMechanic = new CameraFollowMechanic(transform, _playerTransform);
    }

    private void Update()
    {
        _cameraFollowMechanic.Update();
    }
}