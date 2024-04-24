using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class HeroAudio : MonoBehaviour
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
                Debug.Log($"ability to {this.name}");
                _sounds.Add(SoundType.Ability, _ability);
            }
        }

        public void PlayStartTurn()
        {
            int index = Random.Range(0, _startTurn.Length);

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