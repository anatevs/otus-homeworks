using UnityEngine;

public partial class Player
{
    //public void OnEnable()
    //{

    //}

    //public void OnDisable()
    //{

    //}



    //move mechanic
    public class MovementMechanic
    {
        private readonly Transform _transform;
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _speed;

        public MovementMechanic(Transform transform, IAtomicValue<Vector3> direction, IAtomicValue<float> speed)
        {
            _transform = transform;
            _direction = direction;
            _speed = speed;
        }

        public void Update()
        {
            _transform.Translate(_direction.Value * _speed.Value * Time.deltaTime);
        }
    }
}