using Audio;

public class DestroyAudioHandler : BaseHandler<DestroyEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    public DestroyAudioHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(DestroyEvent evnt)
    {
        _visualPipeline.AddTask(new PlaySoundAudioTask(
            SoundType.Death,
            evnt.entity.Get<InfoComponent>(),
            _heroServiceAudio));
    }
}