using UnityEngine;

public partial class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    private CameraFollowMechanic _cameraFollowMechanic;

    public void Awake()
    {
        _cameraFollowMechanic = new CameraFollowMechanic(transform, _playerTransform);
    }

    void Update()
    {
        _cameraFollowMechanic.Update();
    }
}