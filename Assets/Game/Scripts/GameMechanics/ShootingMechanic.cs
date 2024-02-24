using UnityEngine;

public partial class Player
{
    public class ShootingMechanic
    {
        private readonly IAtomicAction _onShoot;
        private readonly AtomicEvent _onShootInput;

        public ShootingMechanic(IAtomicAction onShoot, AtomicEvent onShootInput)
        {
            _onShoot = onShoot;
            _onShootInput = onShootInput;
        }

        public void OnEnable()
        {
            _onShootInput.Subscribe(MakeShoot);
        }

        public void OnDisable()
        {
            _onShootInput.Unsubscribe(MakeShoot);
        }

        private void MakeShoot()
        {
            _onShoot?.Invoke();
            Debug.Log("Shoot!");
        }
    }
}