public sealed class DestoyHandler : BaseHandler<DestroyEvent>
{
    private readonly HeroListService _heroListService;

    public DestoyHandler(EventBus eventBus, HeroListService heroListService) : base(eventBus)
    {
        _heroListService = heroListService;
    }

    protected override void RaiseEvent(DestroyEvent evnt)
    {
        HeroEntity entity = evnt.entity;

        _heroListService.RemoveHero(entity);

        entity.Set(new IsActiveComponent(false));

        entity.gameObject.SetActive(false);
    }
}