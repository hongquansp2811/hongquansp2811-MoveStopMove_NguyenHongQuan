using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music")]
    public AudioSource backgroundMusicSource;
    public AudioClip backgroundMusicClip;

    [Header("Sound Effects")]
    public AudioSource sfxSource;
    public AudioClip deathSound;
    public AudioClip winSound;
    public AudioClip attackSound;
    public AudioClip loseSound;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayDeathSound()
    {
        PlaySFX(deathSound);
    }

    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    public void PlayAttackSound()
    {
        PlaySFX(attackSound);
    }

    public void PlayLoseSound()
    {
        PlaySFX(loseSound);
    }
}
