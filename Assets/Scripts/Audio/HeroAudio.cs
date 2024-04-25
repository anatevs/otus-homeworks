using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;


namespace Audio
{
    public sealed class HeroAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _startTurn;

        [SerializeField]
        private AudioClip _lowHealth;

        [SerializeField]
        private AudioClip _ability;

        [SerializeField]
        private AudioClip _death;

        private readonly Dictionary<SoundType, AudioClip> _sounds = new();

        private AudioPlayer _audioPlayer;

        private void Start()
        {
            _audioPlayer = AudioPlayer.Instance;

            _sounds.Add(SoundType.LowHealth, _lowHealth);
            _sounds.Add(SoundType.Death, _death);
            if (_ability != null)
            {
                _sounds.Add(SoundType.Ability, _ability);
            }
        }

        public async UniTask PlaySoundAsync(SoundType soundType)
        {
            if (TryGetAudio(soundType, out var audio))
            {
                _audioPlayer.PlaySound(audio);
                await UniTask.Delay(Mathf.RoundToInt(audio.length * 1000));
            }

            await UniTask.Delay(0);
        }


        private bool TryGetAudio(SoundType soundType, out AudioClip audio)
        {
            if (soundType == SoundType.StartTurn)
            {
                int index = UnityEngine.Random.Range(0, _startTurn.Length);

                audio = _startTurn[index];
                return true;
            }

            return _sounds.TryGetValue(soundType, out audio);
        }


        private void PlayStartTurn()
        {
            int index = UnityEngine.Random.Range(0, _startTurn.Length);

            _audioPlayer.PlaySound(_startTurn[index]);
        }

        public void PlaySound(SoundType soundType)
        {
            if (soundType == SoundType.StartTurn)
            {
                PlayStartTurn();
                return;
            }

            if (_sounds.ContainsKey(soundType))
            {
                _audioPlayer.PlaySound(_sounds[soundType]);
            }
        }

    }
}