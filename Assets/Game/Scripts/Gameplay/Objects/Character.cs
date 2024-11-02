using UnityEngine;

namespace SampleGame
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private float _speed = 2.5f;

        [SerializeField]
        private LocationsLoader _locationsLoader;

        private readonly string _triggerString = "[Trigger";

        public void Move(Vector3 direction, float deltaTime)
        {
            transform.position += direction * (deltaTime * _speed);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void OnTriggerEnter(Collider other)
        {
            var name = other.gameObject.name;

            if (name.Contains(_triggerString))
            {
                var locationIndex = name[(_triggerString.Length)..^1];

                _locationsLoader.LoadLocation(locationIndex);
            }
        }
    }
}