public sealed class HeroEntity : Entity
{
    private HeroModel _model;

    private void Awake()
    {
        _model = GetComponent<HeroModel>();

        Add(new HPComponent(_model.HP));
        Add(new DamageComponent(_model.Damage));
        if (_model.HeroAbility != null)
        {
            if (_model.HeroAbility.effect.AbilityType == AbilityType.Weapon)
            {
                Add(new WeaponComponent(_model.HeroAbility));
            }
            else if (_model.HeroAbility.effect.AbilityType == AbilityType.Shield)
            {
                Add(new ShieldComponent(_model.HeroAbility));
            }
        }
    }
}