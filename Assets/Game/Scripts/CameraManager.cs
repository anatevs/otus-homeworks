using UnityEngine;

public partial class CameraManager : 
    MonoBehaviour,
    IFinishGameListener
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

    public void OnFinishGame()
    {
        _cameraFollowMechanic.OnFinishGame();
    }
}