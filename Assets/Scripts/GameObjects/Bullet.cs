using UnityEngine;

namespace Sample
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private MoveComponent moveComponent;
        
        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private float _lifetime = 3;
        
        private void Start()
        {
            this.moveComponent.SetDirection(this.transform.forward);
            Destroy(this.gameObject, _lifetime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LifeComponent lifeComponent))
            {
                lifeComponent.TakeDamage(_damage);
                Destroy(this.gameObject);
            }
        }
    }
}