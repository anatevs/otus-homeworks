public interface IEffect : IEvent
{
    public AbilityType AbilityType { get; }

    public HeroEntity Hero { get; set; }

    public HeroEntity Target { get; set; }
}