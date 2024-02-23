using UnityEngine;

public partial class Player
{
    public class DestroyMechanic
    {
        private readonly GameObject _gameObject;

        private readonly IAtomicEvent _death;

        public DestroyMechanic(GameObject gameObject, IAtomicEvent death)
        {
            _gameObject = gameObject;
            _death = death;
        }

        public void OnEnable()
        {
            _death.Subscribe(OnDeath);
        }

        public void OnDesable()
        {
            _death.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            Object.Destroy(_gameObject);
        }
    }
}