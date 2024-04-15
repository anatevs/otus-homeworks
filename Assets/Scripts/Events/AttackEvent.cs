public readonly struct AttackEvent
{
    public readonly HeroEntity target;
    public readonly HeroEntity hero;

    public AttackEvent(HeroEntity target, HeroEntity hero)
    {
        this.target = target;
        this.hero = hero;
    }
}