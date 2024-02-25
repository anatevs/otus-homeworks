public partial class Player
{
    public class RefillWeaponMechanic
    {
        private readonly IAtomicEvent OnCanRefill;
        private readonly IAtomicVariable<int> _weaponMagazine;
        private readonly IAtomicValue<int> _refillAmout;

        public RefillWeaponMechanic(IAtomicEvent OnCanRefill, IAtomicVariable<int> weaponMagazine, IAtomicValue<int> refillAmout)
        {
            this.OnCanRefill = OnCanRefill;
            _weaponMagazine = weaponMagazine;
            _refillAmout = refillAmout;
        }

        public void OnEnable()
        {
            OnCanRefill.Subscribe(MakeWeaponRefill);
        }

        public void OnDisable()
        {
            OnCanRefill.Unsubscribe(MakeWeaponRefill);
        }

        private void MakeWeaponRefill()
        {
            _weaponMagazine.Value += _refillAmout.Value;
        }
    }
}