using Audio;
using System.Collections.Generic;
using UnityEngine;

public sealed class LowHealthAudioHandler : BaseHandler<DefaultDealDamageEvent>
{
    private readonly AudioVisualPipeline _visualPipeline;
    private readonly HeroServiceAudio _heroServiceAudio;

    private readonly float _thresholdLevel = 0.2f;

    private readonly HashSet<InfoComponent> _alarmedBefore = new();

    public LowHealthAudioHandler(EventBus eventBus, AudioVisualPipeline visualPipeline, HeroServiceAudio heroServiceAudio) : base(eventBus)
    {
        _visualPipeline = visualPipeline;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override void RaiseEvent(DefaultDealDamageEvent evnt)
    {
        int hp = evnt.entity.Get<HPComponent>().CurrentHP;
        int initHP = evnt.entity.Get<HPComponent>().InitHP;
        InfoComponent info = evnt.entity.Get<InfoComponent>();

        if (hp <= initHP * _thresholdLevel && !_alarmedBefore.Contains(info))
        {
            Debug.Log($"{info.team} number {info.id} is less than 20%");

            _visualPipeline.AddTask(new PlaySoundAudioTask(
            SoundType.LowHealth,
            evnt.entity.Get<InfoComponent>(),
            _heroServiceAudio));

            _alarmedBefore.Add(info);
        }
    }
}