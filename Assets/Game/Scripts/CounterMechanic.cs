using UnityEngine;

public partial class Player
{
    public class CounterMechanic
    {
        private IAtomicAction _onCounted;
        private IAtomicValue<float> _count;
        private float _timer;

        public CounterMechanic(IAtomicAction onCounted, IAtomicValue<float> count)
        {
            _onCounted = onCounted;
            _count = count;
            _timer = count.Value;
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if ( _timer > 0 )
            {
                return;
            }
            else
            {
                _timer = _count.Value;
                _onCounted?.Invoke();
            }
        }
    }
}