using UnityEngine;

namespace Assets
{
    public class Sounds : MonoBehaviour {

        public AudioClip MainTheme;
        public AudioClip DeathTheme;
        public AudioClip Death;
        public AudioClip StartGame;
        public AudioSource[] Collect;

        public AudioSource Music;
        public AudioSource Sfx;

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
    }
}
