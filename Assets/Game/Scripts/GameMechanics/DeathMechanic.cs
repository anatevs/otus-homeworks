using UnityEngine;

public partial class Player
{
    //death mechanic
    public class DeathMechanic
    {
        private readonly IAtomicAction _onDeath;
        private readonly IAtomicValue<int> _hp;
        private readonly GameObject _gameObject;

        public DeathMechanic(IAtomicAction onDeath, IAtomicValue<int> hp, GameObject gameObject)
        {
            _onDeath = onDeath;
            _hp = hp;
            _gameObject = gameObject;
        }

        public void Update()
        {
            if (_hp.Value > 0)
            {
                return;
            }
            else
            {
                _onDeath?.Invoke();
            }
        }
    }
}