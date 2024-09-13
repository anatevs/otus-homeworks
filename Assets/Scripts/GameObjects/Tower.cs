using UnityEngine;

namespace Sample
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private RotationComponent _rotationComponent;
        
        [SerializeField]
        private LifeComponent _lifeComponent;
        
        [SerializeField]
        private ShootComponent _shootComponent;

        private void Awake()
        {
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);
        }

        private void FixedUpdate()
        {
            if (!_lifeComponent.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
    }
}