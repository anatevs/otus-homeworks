using Sounds;

public class LowHealthAudioHandler : BaseHandler<DefaultDealDamageEvent>
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    private readonly float _thresholdLevel = 0.2f;

    public LowHealthAudioHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(DefaultDealDamageEvent evnt)
    {
        int hp = evnt.entity.Get<HPComponent>().CurrentHP;
        int initHP = evnt.entity.Get<HPComponent>().InitHP;

        if (hp <= initHP * _thresholdLevel)
        {
            _visualPipeline.AddTask(new PlaySoundAudioTask(
            SoundType.LowHealth,
            evnt.entity.Get<InfoComponent>(),
            _heroServiceAudio));
        }
    }
}