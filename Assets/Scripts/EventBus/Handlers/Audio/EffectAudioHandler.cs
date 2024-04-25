using Audio;

public class EffectAudioHandler<T> : BaseHandler<T> where T : IEffect
{
    private readonly VisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    public EffectAudioHandler(EventBus eventBus, VisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(T evnt)
    {
        InfoComponent info = new();

        if (evnt is IAttackEffect attackEffect)
        {
            info = attackEffect.Hero.Get<InfoComponent>();
        }
        else if (evnt is IDefenceEffect defenceEffect)
        {
            info = defenceEffect.Target.Get<InfoComponent>();
        }

        _visualPipeline.AddTask(new PlaySoundAudioTask(
        SoundType.Ability, info, _heroServiceAudio));

    }
}