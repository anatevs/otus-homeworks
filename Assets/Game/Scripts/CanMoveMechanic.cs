public partial class Player
{
    public class CanMoveMechanic
    {
        private readonly AtomicVariable<bool> _isDead;
        private readonly AtomicVariable<bool> _canMove;

        public CanMoveMechanic(AtomicVariable<bool> isDead, AtomicVariable<bool> canMove)
        {
            _isDead = isDead;
            _canMove = canMove;
        }

        public void OnEnable()
        {
            _isDead.Subscribe(OnDeadChanged);
        }

        public void OnDisable()
        {
            _isDead.Unsubscribe(OnDeadChanged);
        }

        private void OnDeadChanged(bool isDead)
        {
            _canMove.Value = !isDead;
        }
    }
}