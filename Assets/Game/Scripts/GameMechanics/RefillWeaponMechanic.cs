public partial class Player
{
    public class RefillWeaponMechanic
    {
        private readonly AtomicEvent _canRefill;
        private readonly IAtomicVariable<int> _weaponMagazine;
        private readonly IAtomicValue<int> _refillAmout;

        public RefillWeaponMechanic(AtomicEvent canRefill, IAtomicVariable<int> weaponMagazine, IAtomicValue<int> refillAmout)
        {
            _canRefill = canRefill;
            _weaponMagazine = weaponMagazine;
            _refillAmout = refillAmout;
        }

        public void OnEnable()
        {
            _canRefill.Subscribe(MakeWeaponRefill);
        }

        public void OnDisable()
        {
            _canRefill.Unsubscribe(MakeWeaponRefill);
        }

        private void MakeWeaponRefill()
        {
            //_weaponMagazine.Value += _refillAmout.Value;
            //Debug.Log($"new magazine value: ");
        }
    }
}