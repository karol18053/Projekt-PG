using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Helper_Scripts
{
    class MusicHelper : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip [] sounds;

        float noMusicTime = 0f;
        float stopMusic = 4f;

        private int iterator = 0;

        private void Update ()
        {
            if ( !audioSource.isPlaying )
            {
                noMusicTime+=Time.deltaTime;

                if ( noMusicTime >= stopMusic )
                {
                    if ( iterator != sounds.Length-1 )
                        iterator++;
                    else
                        iterator = 0;

                    audioSource.clip = sounds [iterator];
                    audioSource.Play ();

                    noMusicTime = 0f;
                }
            }
        }

        private void Awake ()
        {
            audioSource = GetComponent<AudioSource> ();
            if ( sounds.Length != 0 )
            {
                audioSource.clip = sounds [iterator];
                audioSource.Play ();
                audioSource.volume = 0.1f;
            }
        }
    }
}
