using UnityEngine;

namespace Sample
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        
        private ShootComponent _shootComponent;

        private void Awake()
        {
            _shootComponent = _character.GetComponent<ShootComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shootComponent.Shoot();        
            }
        }
    }
}