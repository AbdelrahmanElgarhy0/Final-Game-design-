using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip collectSound;
    public AudioClip hitSound;
    
    public void PlayMusicIfNotPlaying()
{
    if (!musicSource.isPlaying)
    {
        PlayMusic();
    }
    
}


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void RestartMusic()
{
    StopMusic();
    PlayMusic();
}


    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayCollect()
    {
        sfxSource.PlayOneShot(collectSound);
    }

    public void PlayHit()
    {
        sfxSource.PlayOneShot(hitSound);
        StopMusic(); // 🔴 stop background music on hit
    }

    // 🎚 VOLUME CONTROL
    public void SetVolume(float value)
    {
        musicSource.volume = value;
        sfxSource.volume = value;
    }

    // 🔇 MUTE
    public void SetMute(bool isMuted)
    {
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;
    }
}
