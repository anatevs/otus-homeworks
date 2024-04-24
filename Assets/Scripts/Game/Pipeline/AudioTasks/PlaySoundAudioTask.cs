using Sounds;

public class PlaySoundAudioTask : Task
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

    protected override void OnRun()
    {
        _heroServiceAudio.PlaySound(_infoComponent, _soundType);

        Finish();
    }
}