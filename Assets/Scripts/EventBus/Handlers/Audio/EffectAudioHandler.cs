using Audio;

public sealed class EffectAudioHandler<T> : BaseHandler<T> where T : IEffect
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    public EffectAudioHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(T evnt)
    {
        TeamInfoComponent info = new();

        if (evnt is IAttackEffect attackEffect)
        {
            info = attackEffect.Hero.Get<TeamInfoComponent>();
        }
        else if (evnt is IDefenceEffect defenceEffect)
        {
            info = defenceEffect.Target.Get<TeamInfoComponent>();
        }

        _visualPipeline.AddTask(new PlaySoundAudioTask(
        SoundType.Ability, info, _heroServiceAudio));

    }
}