using UnityEngine;
public class CameraFollowMechanic
{
    private readonly Transform _followerTransform;
    private readonly Transform _followingTransform;

    private readonly Vector3 _offset;

    public CameraFollowMechanic(Transform followerTransform, Transform followingTransform)
    {
        _followerTransform = followerTransform;
        _followingTransform = followingTransform;

        _offset = _followingTransform.position - _followerTransform.position;
    }

    public void Update()
    {
        _followerTransform.position = _followingTransform.position - _offset;
    }
}