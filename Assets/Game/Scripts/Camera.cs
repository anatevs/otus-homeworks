using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // follow the player mechanic
    public class CameraFollowMechanic
    {
        private readonly Transform _followerTransform;
        private readonly Transform _followingTransform;
        private readonly Vector3 _offset;

        public CameraFollowMechanic(Transform followerTransform, Transform followingTransform, Vector3 offset)
        {
            _followerTransform = followerTransform;
            _followingTransform = followingTransform;
            _offset = offset;
        }

        public void Update()
        {
            _followerTransform.position = _followingTransform.position + _offset;
        }
    }
}
