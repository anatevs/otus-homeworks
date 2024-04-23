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
            if (_model.HeroAbility.effect is IAttackEffect attackEffect)
            {
                Add(new WeaponComponent(attackEffect));
            }
            else if (_model.HeroAbility.effect is IDefenceEffect defenceEffect)
            {
                Add(new ShieldComponent(defenceEffect));
            }
        }
    }
}