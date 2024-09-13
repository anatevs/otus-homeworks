using UnityEngine;

namespace Sample
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;
        
        private void Awake()
        {
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
        }

        private void FixedUpdate()
        {
            if (_moveComponent.Direction != Vector3.zero)
            {
                _rotationComponent.Rotate(_moveComponent.Direction);
            }
            
            if (!_lifeComponent.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
    }
}