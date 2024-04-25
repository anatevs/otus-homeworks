using Audio;

public sealed class PlaySoundAudioTask : Task
{
    private readonly SoundType _soundType;
    private readonly InfoComponent _infoComponent;
    private readonly HeroServiceAudio _heroServiceAudio;

    public PlaySoundAudioTask(SoundType soundType, InfoComponent infoComponent, HeroServiceAudio heroServiceAudio)
    {
        _soundType = soundType;
        _infoComponent = infoComponent;
        _heroServiceAudio = heroServiceAudio;
    }

    protected override async void OnRun()
    {
        await _heroServiceAudio.PlaySoundTask(_infoComponent, _soundType);

        Finish();
    }
}