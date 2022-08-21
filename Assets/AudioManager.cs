using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Play(AudioClip clip, float volume)
    {
        sfxSource.clip = clip;
        sfxSource.loop = false;
        sfxSource.volume = 1.0f;
        sfxSource.Play();
    }
}
