public interface IEffect : IEvent
{
    public HeroEntity Target { get; set; }
}