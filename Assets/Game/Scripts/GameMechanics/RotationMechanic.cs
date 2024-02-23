using UnityEngine;

public partial class Player
{
    //rotation mechanic
    public class RotationMechanic
    {
        private readonly Transform _transform;
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _rotSpeed;

        public RotationMechanic(Transform transform, IAtomicValue<Vector3> direction, IAtomicValue<float> rotSpeed)
        {
            _transform = transform;
            _direction = direction;
            _rotSpeed = rotSpeed;
        }

        public void Update()
        {
            Quaternion lookQuaternion = Quaternion.LookRotation(_direction.Value);
            Quaternion.Slerp(_transform.rotation, lookQuaternion, _rotSpeed.Value * Time.deltaTime);
        }
    }
}