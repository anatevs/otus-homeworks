using UnityEngine;
public class CameraFollowMechanic
{
    private readonly Transform _followerTransform;
    private readonly Transform _followingTransform;

    private readonly Vector3 _offset;
    private bool _isPlaying;

    public CameraFollowMechanic(Transform followerTransform, Transform followingTransform)
    {
        _followerTransform = followerTransform;
        _followingTransform = followingTransform;

        _isPlaying = true;
        _offset = _followingTransform.position - _followerTransform.position;
    }

    public void Update()
    {
        if (_isPlaying)
        {
            _followerTransform.position = _followingTransform.position - _offset;
        }
    }

    public void OnFinishGame()
    {
        _isPlaying = false;
    }
}