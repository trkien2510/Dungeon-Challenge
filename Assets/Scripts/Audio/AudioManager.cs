using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgMusic;
    public AudioClip swordSwing;
    public AudioClip skeletonDead;

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bgmSource.clip = bgMusic;
        bgmSource.loop = true;
        bgmSource.Play();

        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        SetBGMVolume(savedVolume);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetBGMVolume(float sliderValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
