public partial class Player
{
    //bullets refill mechanic
    public class RefillWeaponMechanic
    {
        private readonly IAtomicEvent<bool> _canRefill;
        private readonly IAtomicVariable<int> _weaponMagazine;
        private readonly IAtomicValue<int> _refillAmout;

        public RefillWeaponMechanic(IAtomicEvent<bool> canRefill, IAtomicVariable<int> weaponMagazine, IAtomicValue<int> refillAmout)
        {
            _canRefill = canRefill;
            _weaponMagazine = weaponMagazine;
            _refillAmout = refillAmout;
        }

        public void OnEnable()
        {
            _canRefill.Subscribe(MakeWeaponRefill);
        }

        public void OnDesable()
        {
            _canRefill.Unsubscribe(MakeWeaponRefill);
        }

        private void MakeWeaponRefill(bool canRefill)
        {
            _weaponMagazine.Value += _refillAmout.Value;
        }
    }
}