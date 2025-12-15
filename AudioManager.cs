using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource voiceSource;

    [Header("SFX")]
    public AudioClip punch;
    public AudioClip hitSmall;
    public AudioClip hitBig;
    public AudioClip burgerDestroy;
    public AudioClip shopBuy;
    public AudioClip cantAfford;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip, float pitchMin = 0.95f, float pitchMax = 1.05f)
    {
        sfxSource.pitch = Random.Range(pitchMin, pitchMax);
        sfxSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.Stop();
        voiceSource.PlayOneShot(clip);
    }
}
