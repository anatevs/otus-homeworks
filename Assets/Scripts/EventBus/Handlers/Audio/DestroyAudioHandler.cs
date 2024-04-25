using Audio;

public sealed class DestroyAudioHandler : BaseHandler<DestroyEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    public DestroyAudioHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
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