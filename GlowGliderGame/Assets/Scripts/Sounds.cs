using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioSourceHelper
    {
        public AudioSource AudioSource;
        public float UnmutedVolume;
    }
    public class Sounds : MonoBehaviour {

        public AudioClip MainTheme;
        public AudioClip DeathTheme;
        public AudioClip Death;
        public AudioClip StartGame;
        public AudioSource[] Collect;

        public AudioSource PowerUp;
        public AudioSource Diamond;

        public AudioSource Music;
        public AudioSource Sfx;

        private List<AudioSource> _audioSources;
        private List<AudioSourceHelper> _audioSourceHelpers;

        public void Setup()
        {
            _audioSources = new List<AudioSource>
            {
                PowerUp,
                Diamond,
                Music,
                Sfx
            };
            _audioSources.AddRange(Collect);

            _audioSourceHelpers = new List<AudioSourceHelper>();

            foreach (var audioSource in _audioSources)
            {
                _audioSourceHelpers.Add(new AudioSourceHelper
                {
                    AudioSource = audioSource,
                    UnmutedVolume = audioSource.volume
                });
            }
        }

        public void Mute()
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.volume = 0;
            }
        }

        public void Unmute()
        {
            for(int i=0; i< _audioSources.Count; i++)
            {
                _audioSources[i].volume = _audioSourceHelpers[i].UnmutedVolume;
            }
        }


        public void PlayMainTheme(bool play)
        {
            Music.clip = MainTheme;
            if (play)
            {
                Music.Play();
            }
            else
            {
                Music.Stop();
            }
        }

        public void PlayDeathTheme(bool play)
        {
            Music.clip = DeathTheme;
            if (play)
            {
                Music.Play();
            }
            else
            {
                Music.Stop();
            }
        }

        public void PlayCollectSfx(int index)
        {
            Collect[index].Play();
        }

        public void PlayDeathSfx()
        {
            Sfx.clip = Death;
            Sfx.Play();
        }

        public void PlaySartGameSfx()
        {
            Sfx.clip = StartGame;
            Sfx.Play();
        }

        public void PlayDiamondSfx()
        {
            Diamond.Play();
        }

        public void PlayPowerUpSfx()
        {
            PowerUp.Play();
        }
    }
}
