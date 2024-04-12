public class HeroEntity : Entity
{
    private HeroModel _model;

    private void Awake()
    {
        _model = GetComponent<HeroModel>();

        Add(new IsActiveComponent(false));
        Add(new HPComponent(_model.HP));
        Add(new DamageComponent(_model.Damage));
    }
}