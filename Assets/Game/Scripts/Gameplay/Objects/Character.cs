using UnityEngine;

namespace SampleGame
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private float _speed = 2.5f;

        [SerializeField]
        private LocationsLoader _locationsLoader;

        public void Move(Vector3 direction, float deltaTime)
        {
            transform.position += direction * (deltaTime * _speed);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var name = other.gameObject.name;

            if (TriggersNames.IsTriggerName(name))
            {
                var locationIndex = TriggersNames.GetTriggerIndex(name);

                _locationsLoader.LoadLocation(locationIndex);
            }
        }
    }
}