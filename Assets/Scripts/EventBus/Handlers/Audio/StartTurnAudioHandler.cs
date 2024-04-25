using Audio;

public class StartTurnAudioHandler : BaseHandler<ChangeActiveEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    public StartTurnAudioHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(ChangeActiveEvent evnt)
    {
        _visualPipeline.AddTask(new PlaySoundAudioTask(
            SoundType.StartTurn,
            evnt.currentHero.Get<InfoComponent>(),
            _heroServiceAudio));
    }
}